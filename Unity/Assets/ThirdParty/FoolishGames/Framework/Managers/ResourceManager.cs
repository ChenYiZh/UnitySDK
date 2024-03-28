using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SystemBasis<ResourceManager>
{
    public delegate void OnAssetLoadCompleted<T>(T obj) where T : Object;

    public Object Load(string path)
    {
        return Load<Object>(path);
    }

    public T Load<T>(string path) where T : Object
    {
        return Resources.Load(path) as T;
    }

    public IEnumerator LoadAsync(string path, OnAssetLoadCompleted<Object> callback)
    {
        yield return LoadAsync<Object>(path, callback);
    }

    public IEnumerator LoadAsync<T>(string path, OnAssetLoadCompleted<T> callback) where T : Object
    {
        var request = Resources.LoadAsync(path);
        yield return request;
        callback.Invoke(request.asset as T);
    }

    public IEnumerator LoadObjectInstanceAsync(string path, OnAssetLoadCompleted<Object> callback)
    {
        var request = Resources.LoadAsync(path);
        yield return request;
        if (request.asset != null)
        {
            callback.Invoke(GameObject.Instantiate(request.asset));
        }
        else
        {
            callback.Invoke(null);
        }
    }

    public void Release()
    {
        if (GameRoot.Root)
            GameRoot.Root.StartCoroutine(_Release());
    }

    private IEnumerator _Release()
    {
        var request = Resources.UnloadUnusedAssets();
        yield return request;
        System.GC.Collect();
    }
}