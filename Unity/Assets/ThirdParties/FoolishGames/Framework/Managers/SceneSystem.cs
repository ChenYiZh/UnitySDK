using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FoolishGames.Log;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : SystemBasis<SceneSystem>
{
    public Dictionary<string, BaseScene> Scenes { get; private set; }

    private List<BaseScene> LoadingScenes;

    public bool IsLoading { get; private set; }

    private bool doLoad = false;

    public float Progress { get; private set; }

    public override bool Ready => !IsLoading;

    public void Load(bool clearExists, params BaseScene[] scenes)
    {
        Load(clearExists, false, scenes);
    }

    public void Load(bool clearExists, bool force, params BaseScene[] scenes)
    {
        if (clearExists)
        {
            if (scenes == null || IsLoading)
            {
                return;
            }
            
            UI_Tips.Show(ETips.Tips_Loading);
            
            List<BaseScene> waitToLoad = new List<BaseScene>(scenes.Length);
            HashSet<string> exists = new HashSet<string>();
            foreach (var scene in scenes)
            {
                if (!force && Scenes.ContainsKey(scene.SceneName))
                {
                    exists.Add(scene.SceneName);
                    continue;
                }

                waitToLoad.Add(scene);
            }

            BaseScene[] waitToRemove;
            if (exists.Count > 0)
            {
                waitToRemove = Scenes.Values.Where(v => !exists.Contains(v.SceneName)).ToArray();
            }
            else
            {
                waitToRemove = Scenes.Values.ToArray();
            }

            Unload(waitToRemove);

            foreach (var scene in waitToLoad)
            {
                Progress = 0;
                IsLoading = true;
                doLoad = true;
                Scenes.Add(scene.SceneName, scene);
                LoadingScenes.Add(scene);
                GameRoot.Root.StartCoroutine(scene.LoadAsync());
            }
        }
        else
        {
            LoadNew(force, scenes);
        }
    }

    public void Load(params BaseScene[] scenes)
    {
        LoadNew(false, scenes);
    }

    public void LoadNew(bool force, params BaseScene[] scenes)
    {
        if (scenes == null || IsLoading)
        {
            return;
        }

        //UI_Tips.Show(ETips.Tips_Loading);

        foreach (var scene in scenes)
        {
            if (!force && Scenes.ContainsKey(scene.SceneName))
            {
                continue;
            }

            Progress = 0;
            IsLoading = true;
            doLoad = true;
            Scenes.Add(scene.SceneName, scene);
            LoadingScenes.Add(scene);
            GameRoot.Root.StartCoroutine(scene.LoadAsync());
        }

        if (!IsLoading)
        {
            UI_Tips.Hide(ETips.Tips_Loading);
        }
    }

    public void Unload(params BaseScene[] scenes)
    {
        if (scenes == null || IsLoading)
        {
            return;
        }

        foreach (var scene in scenes)
        {
            if (!Scenes.ContainsKey(scene.SceneName))
            {
                continue;
            }

            Progress = 0;
            LoadingScenes.Add(scene);
            IsLoading = true;
            doLoad = false;
            Scenes.Remove(scene.SceneName);
            GameRoot.Root.StartCoroutine(scene.UnloadAsync());
        }
    }

    private void LoadCompleted()
    {
        EventManager.Instance.Send(Events.SceneLoaded);
    }

    private void UnloadCompleted()
    {
        EventManager.Instance.Send(Events.SceneUnloaded);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (IsLoading)
        {
            bool isDone = LoadingScenes.All(s => s.IsDone);
            Progress = LoadingScenes.Sum(s => s.Progress) / LoadingScenes.Count;
            if (!isDone)
            {
                //FConsole.WriteError(string.Join(",", LoadingScenes.Select(s => s.SceneName)));
                EventManager.Instance.Send(Events.LoadingProgress, new TipsLoadingProgress() { Progress = Progress });
            }

            if (isDone)
            {
                Progress = 1;
                IsLoading = false;
                LoadingScenes.Clear();
                if (doLoad)
                {
                    LoadCompleted();
                }
                else
                {
                    UnloadCompleted();
                }
            }
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        Scenes = new Dictionary<string, BaseScene>();
        LoadingScenes = new List<BaseScene>();
        IsLoading = false;
        doLoad = false;
    }
}