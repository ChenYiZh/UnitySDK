﻿using System;
using System.Collections;
using System.Collections.Generic;
using FoolishGames.Log;
using UnityEngine;

#region 内部函数

public partial class UI_Tips : MonoBehaviour, IUpdate
{
    private static UI_Tips _instance;

    public static UI_Tips Root
    {
        get { return _instance; }
    }

    public Dictionary<ETips, TipsPanel> Panels { get; private set; }

    public Dictionary<ETips, Coroutine> Coroutines { get; private set; }

    private void Awake()
    {
        _instance = this;
        Panels = new Dictionary<ETips, TipsPanel>();
        Coroutines = new Dictionary<ETips, Coroutine>();
        Initialize();
        foreach (var kv in Panels)
        {
            kv.Value.Visible = false;
            try
            {
                kv.Value.OnRegist(this);
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }

            kv.Value.gameObject.SetActive(false);
        }

        foreach (var kv in Panels)
        {
            if (kv.Value.InitializeOnAwake)
            {
                kv.Value.Initialized = true;
                try
                {
                    kv.Value.OnInitialize();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
    }

    float lastTime;

    private void Update()
    {
        try
        {
            OnUpdate();
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }

        if ((lastTime += Time.deltaTime) >= 1)
        {
            lastTime = 0;
            try
            {
                OnSecond();
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }
    }

    private void LateUpdate()
    {
        try
        {
            OnLateUpdate();
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }
    }

    private void FixedUpdate()
    {
        try
        {
            OnFixedUpdate();
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }
    }

    private void OnDestroy()
    {
        //EventManager.Instance.UnregistEvent(Events.UseGM, OnUseGMReceive);
        foreach (var kv in Panels)
        {
            if (kv.Value.enabled)
            {
                try
                {
                    kv.Value.OnDestroy();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
    }

    public void OnFixedUpdate()
    {
        foreach (var kv in Panels)
        {
            if (kv.Value.enabled)
            {
                try
                {
                    kv.Value.OnFixedUpdate();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
    }

    public void OnLateUpdate()
    {
        foreach (var kv in Panels)
        {
            if (kv.Value.enabled)
            {
                try
                {
                    kv.Value.OnLateUpdate();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
    }

    public void OnUpdate()
    {
        foreach (var kv in Panels)
        {
            if (kv.Value.enabled)
            {
                try
                {
                    kv.Value.OnUpdate();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
    }

    public void OnSecond()
    {
        foreach (var kv in Panels)
        {
            if (kv.Value.enabled)
            {
                try
                {
                    kv.Value.OnSecond();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
    }

    private void Regist(TipsPanel panel)
    {
        if (panel == null || Panels.ContainsKey(panel.TipsID))
        {
            FConsole.WriteError("Tips注册有问题！");
            return;
        }

        Panels.Add(panel.TipsID, panel);
    }

    public static void Show(ETips tipsID, TipsParam param = null)
    {
        Root._Show(tipsID, param);
    }

    private void _Show(ETips tipsID, TipsParam param = null)
    {
        Show(tipsID, true, param);
    }

    public static void Show(ETips tipsID, bool hideOthers, TipsParam param = null)
    {
        Root._Show(tipsID, hideOthers, param);
    }

    private void _Show(ETips tipsID, bool hideOthers, TipsParam param = null)
    {
        if (!Panels.ContainsKey(tipsID))
        {
            return;
        }

        var panel = Panels[tipsID];
        if (hideOthers && panel.HideOthers)
        {
            foreach (var kv in Panels)
            {
                if (kv.Value.Visible && !kv.Value.IgnoreHideOthers && kv.Key != tipsID)
                {
                    StartCoroutine(_Hide(kv.Value));
                }
            }
        }

        if (Coroutines.ContainsKey(tipsID))
        {
            StopCoroutine(Coroutines[tipsID]);
            Coroutines.Remove(tipsID);
        }

        var coroutine = StartCoroutine(_Show(panel, param));
        if (coroutine != null)
        {
            Coroutines.Add(tipsID, coroutine);
        }
    }

    private IEnumerator _Show(TipsPanel panel, TipsParam param)
    {
        if (!panel.Initialized)
        {
            panel.Initialized = true;
            try
            {
                panel.OnInitialize();
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }

        panel.gameObject.SetActive(true);
        try
        {
            panel.BeforePlayDisplayAnimation();
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }

        panel.Visible = true;
        if (panel.AnimConfig.AnimIn != EUIAnim.None)
        {
            yield return Util.PlayAnim(panel, panel.AnimConfig, true);
        }
        else
        {
            var canvas = panel.GameObject.GetComponent<CanvasGroup>();
            canvas.alpha = 1;
        }

        try
        {
            panel.OnShow(param);
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }

        if (Coroutines.ContainsKey(panel.TipsID))
        {
            Coroutines.Remove(panel.TipsID);
        }
    }

    public static void Hide(ETips tipsID)
    {
        Root._Hide(tipsID);
    }

    private void _Hide(ETips tipsID)
    {
        if (!Panels.ContainsKey(tipsID))
        {
            return;
        }

        var panel = Panels[tipsID];
        if (!panel.GameObject.activeSelf)
        {
            return;
        }

        if (Coroutines.ContainsKey(tipsID))
        {
            StopCoroutine(Coroutines[tipsID]);
            Coroutines.Remove(tipsID);
        }

        var coroutine = StartCoroutine(_Hide(panel));
        if (coroutine != null)
        {
            Coroutines.Add(tipsID, coroutine);
        }
    }

    private IEnumerator _Hide(TipsPanel panel)
    {
        panel.Visible = false;
        try
        {
            panel.BeforePlayHideAnimation();
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }

        if (panel.AnimConfig.AnimOut != EUIAnim.None)
        {
            yield return Util.PlayAnim(panel, panel.AnimConfig, false);
        }
        else
        {
            var canvas = panel.GameObject.GetComponent<CanvasGroup>();
            canvas.alpha = 0;
        }

        try
        {
            panel.OnHide();
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }

        panel.gameObject.SetActive(false);
        Coroutines.Remove(panel.TipsID);
    }

    // #region Tips_GM
    // private GameObject Tips_GM;
    //
    // private void OnUseGMReceive(EventParam param = null)
    // {
    //     if (GameDefines.UseGM)
    //     {
    //         // if (Tips_GM == null)
    //         // {
    //         //     Tips_GM = Util.Instantiate<GameObject>(ResourceManager.Instance.Load(GameSettings.Instance.ResourceUITipsPath + "Tips_GM"), GameRoot.Root.UIRoot);
    //         //     Util.GetOrAddComponent<Tips_GM>(Tips_GM);
    //         // }
    //     }
    //     else
    //     {
    //         if (Tips_GM != null)
    //         {
    //             Tips_GM.SetActive(false);
    //             GameObject.DestroyImmediate(Tips_GM);
    //             Tips_GM = null;
    //             ResourceManager.Instance.Release();
    //         }
    //     }
    // }
    // #endregion
}

#endregion

public abstract class TipsPanel : IUpdate, ICustomAnim
{
    public abstract ETips TipsID { get; }

    /// <summary>
    /// UI路径
    /// </summary>
    public abstract string Path { get; }

    /// <summary>
    /// 显示面板时是否关闭其他面板
    /// </summary>
    public virtual bool HideOthers
    {
        get { return true; }
    }

    /// <summary>
    /// 是否忽视被动关闭面板
    /// </summary>
    public virtual bool IgnoreHideOthers
    {
        get { return false; }
    }

    /// <summary>
    /// 是否一启动就初始化
    /// </summary>
    public virtual bool InitializeOnAwake
    {
        get { return false; }
    }

    public virtual AnimConfig AnimConfig { get; protected set; } = AnimConfig.TipsDefault;

    /// <summary>
    /// 是否初始化完成
    /// </summary>
    public bool Initialized { get; set; }

    /// <summary>
    /// 是否还处于显示状态
    /// </summary>
    public bool Visible { get; set; }

    /// <summary>
    /// 是否执行Update
    /// </summary>
    public virtual bool enabled
    {
        get { return Visible; }
        protected set { }
    }

    public virtual GameObject GameObject
    {
        get { return gameObject; }
        protected set { }
    }

    public GameObject gameObject { get; private set; }

    public Transform transform { get; private set; }

    public virtual void OnRegist(UI_Tips uiTips)
    {
        gameObject = uiTips.transform.Find(Path).gameObject;
        transform = gameObject.transform;
    }

    public void Show(TipsParam param = null)
    {
        UI_Tips.Show(TipsID, param);
    }

    public virtual void OnShow(TipsParam param = null)
    {
    }

    public void Hide()
    {
        UI_Tips.Hide(TipsID);
    }

    public virtual void OnHide()
    {
    }

    public virtual void OnInitialize()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnLateUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnSecond()
    {
    }

    public virtual void BeforePlayDisplayAnimation()
    {
    }

    public virtual IEnumerator PlayCustomDisplayAnimation()
    {
        yield return null;
    }

    public virtual void BeforePlayHideAnimation()
    {
    }

    public virtual IEnumerator PlayCustomHideAnimation()
    {
        yield return null;
    }

    public virtual void OnDestroy()
    {
    }
}

public class TipsParam
{
}