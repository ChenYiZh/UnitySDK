using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILazyObject : IRelease
{
    bool Exists { get; }

    void Load();

    GameObject gameObject { get; }

    Transform transform { get; }
}

public abstract class LazyObject : ILazyObject
{
    public bool Exists { get; private set; }

    private GameObject _gameObject { get; set; }

    private bool _operating { get; set; }

    public GameObject gameObject
    {
        get
        {
            if (OnAnyEvent())
            {
                return _gameObject;
            }
            else
            {
                return null;
            }
        }
    }

    public Transform transform { get { return gameObject == null ? null : gameObject.transform; } }

    protected bool OnAnyEvent()
    {
        if (_operating)
        {
            return _gameObject;
        }
        if (Exists)
        {
            if (_gameObject)
            {
                return true;
            }
            else
            {
#if UNITY_EDITOR
                throw new NullReferenceException();
#endif
                return false;
            }
        }
        else
        {
            _operating = true;
            try
            {
                _gameObject = Instantiate();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            _operating = false;
            Exists = true;
            if (_gameObject)
            {
                return true;
            }
            else
            {
#if UNITY_EDITOR
                OnCreateFailed();
#endif
                return false;
            }
        }
    }

    protected virtual void OnCreateFailed()
    {
        throw new NullReferenceException("GameObject 懒加载失败!");
    }

    public virtual void Release()
    {
        if (Exists)
        {
            Exists = false;
            _operating = true;
            if (_gameObject)
            {
                try
                {
                    OnRelease();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                _gameObject = null;
            }
            _operating = false;
        }
    }

    public void Load()
    {
        OnAnyEvent();
    }

    protected abstract void OnRelease();

    protected abstract GameObject Instantiate();

    protected void Create()
    {
        Exists = false;
        _operating = false;
    }
}