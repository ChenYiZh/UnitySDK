using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

public class GameObjectPoolItem : LazyObject
{
    public GameObjectPool Pool { get; private set; }

    private GameObject _gameObject { get; set; }

    protected GameObjectPoolItem(GameObjectPool pool)
    {
        Pool = pool;
    }

    internal static GameObjectPoolItem New(GameObjectPool pool)
    {
        return new GameObjectPoolItem(pool);
    }

    protected override GameObject Instantiate()
    {
        _gameObject = _gameObject;
        if (!_gameObject)
        {
            if (Pool.TotalCount == 0 && Pool.CanUseDefaultItem)
            {
                _gameObject = Pool.DefaultObject;
            }
            else
            {
                _gameObject = GameObject.Instantiate(Pool.DefaultObject, Pool.DefaultObject.transform.parent);
            }
        }
        GameObjectPool.Use(Pool, this);
        _gameObject.SetActive(true);
        return _gameObject;
    }

    protected override void OnRelease()
    {
        gameObject.SetActive(false);
        GameObjectPool.Collect(Pool, this);
    }

    internal GameObject GetGameObject()
    {
        return _gameObject;
    }
}

public class GameObjectPool : IRelease, IReset
{
    internal GameObject DefaultObject { get; private set; }

    private ILazyObject _default { get; set; }

    public int TotalCount { get { return _usingItems.Count + _collection.Count; } }

    private bool _canUseDefaultItem;
    public bool CanUseDefaultItem
    {
        get { return _canUseDefaultItem; }
        set
        {
            if (value)
            {
                if (!(_usingItems.Contains(_default) || _collection.Contains(_default)))
                {
                    if (_default.Exists)
                    {
                        _usingItems.Add(_default);
                    }
                    else
                    {
                        _collection.Add(_default);
                    }
                }
            }
            else
            {
                _default.Release();
                if (_usingItems.Contains(_default))
                {
                    _usingItems.Remove(_default);
                }
                if (_collection.Contains(_default))
                {
                    _collection.Remove(_default);
                }
            }
        }
    }

    public Transform Parent { get; set; }

    private HashSet<ILazyObject> _usingItems;
    private HashSet<ILazyObject> _collection;

    public GameObjectPool(GameObject item, bool canUseDefaultItem = true)
    {
        DefaultObject = item;
        _usingItems = new HashSet<ILazyObject>();
        _collection = new HashSet<ILazyObject>();
        _default = GameObjectPoolItem.New(this);
        _default.Release();
        CanUseDefaultItem = canUseDefaultItem;
        _collection.Clear();
        Parent = DefaultObject.transform.parent;
    }

    public ILazyObject GetItem()
    {
        ILazyObject obj = null;
        if (_collection.Count > 0)
        {
            foreach (ILazyObject o in _collection)
            {
                obj = o;
                break;
            }
        }
        if (obj == null)
        {
            obj = GameObjectPoolItem.New(this);
        }
        obj.transform.SetParent(Parent);
        Use(this, obj);
        return obj;
    }

    internal static void Use(GameObjectPool pool, ILazyObject obj)
    {
        if (pool._collection.Contains(obj))
        {
            pool._collection.Remove(obj);
        }
        if (!pool._usingItems.Contains(obj))
        {
            pool._usingItems.Add(obj);
        }
    }

    internal static void Collect(GameObjectPool pool, ILazyObject obj)
    {
        if (pool._usingItems.Contains(obj))
        {
            pool._usingItems.Remove(obj);
        }
        if (!pool._collection.Contains(obj))
        {
            pool._collection.Add(obj);
        }
    }

    public void Release()
    {
        List<ILazyObject> objs = new List<ILazyObject>(_usingItems);
        foreach (ILazyObject obj in objs)
        {
            obj.gameObject.SetActive(false);
            obj.Release();
        }
    }

    public void Reset()
    {
        Release();
        List<ILazyObject> objs = new List<ILazyObject>(_collection);
        foreach (ILazyObject obj in objs)
        {
            if (obj.gameObject != DefaultObject)
            {
                GameObject.Destroy(((GameObjectPoolItem)obj).GetGameObject());
            }
        }
        _usingItems.Clear();
        _collection.Clear();
        CanUseDefaultItem = _canUseDefaultItem;
    }
}
