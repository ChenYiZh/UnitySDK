using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseScene
{
    public abstract string SceneName { get; }

    public Scene Scene { get; protected set; }

    public float Progress { get; protected set; }

    public bool IsLoading { get; protected set; } = false;

    public bool IsDone { get; protected set; } = true;

    public bool Wait2Unload { get; protected set; } = false;

    public AsyncOperation AsyncOperation { get; protected set; }

    public IEnumerator LoadAsync()
    {
        if (!IsDone)
        {
            yield break;
        }

        IsLoading = true;
        IsDone = false;
        Progress = 0;
        AsyncOperation = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        //AsyncOperation.allowSceneActivation = false;
        while (!AsyncOperation.isDone)
        {
            yield return null;
            Progress = AsyncOperation.progress;
        }

        LoadCompleted();
    }

    public void Load()
    {
        if (!IsDone)
        {
            return;
        }

        IsLoading = true;
        IsDone = false;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        LoadCompleted();
    }

    private void LoadCompleted()
    {
        AsyncOperation = null;
        if (Wait2Unload)
        {
            Wait2Unload = false;
            UnloadAsync();
            return;
        }

        Scene = SceneManager.GetSceneByName(SceneName);
        IsLoading = false;
        IsDone = true;
        GameRoot.Root.StartCoroutine(WaitToCompleted());
        //OnLoadCompleted();
    }

    private IEnumerator WaitToCompleted()
    {
        while (SceneSystem.Instance.IsLoading)
        {
            yield return null;
        }
        yield return null;
        OnLoadCompleted();
    }

    protected virtual void OnLoadCompleted()
    {
        SceneManager.SetActiveScene(Scene);
        //UI_Tips.Hide(ETips.Tips_Loading);
        //等一帧是因为可能Event消息还在队列里
        //GameRoot.Root.StartCoroutine(OnLoadCompletedTask());
    }

    protected IEnumerator OnLoadCompletedTask()
    {
        yield return null;
        yield return null;
        yield return null;
        UI_Tips.Hide(ETips.Tips_Loading);
    }

    public IEnumerator UnloadAsync()
    {
        if (AsyncOperation != null)
        {
            Wait2Unload = true;
            yield break;
        }

        AsyncOperation = SceneManager.UnloadSceneAsync(Scene);
        IsDone = false;
        yield return AsyncOperation;
        UnloadCompleted();
    }

    private void UnloadCompleted()
    {
        AsyncOperation = null;
        Wait2Unload = false;
        IsDone = true;
        OnUnloadCompleted();
    }

    protected virtual void OnUnloadCompleted()
    {
    }

    // public virtual IEnumerator Reload()
    // {
    //     yield return UnloadAsync();
    //     yield return LoadAsync();
    // }
}