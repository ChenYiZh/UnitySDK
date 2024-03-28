﻿using System;
using System.Collections;
using System.Collections.Generic;
using FoolishGames.Log;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIBasePanel : MonoBehaviour, IVisual, ICustomAnim
{
    public abstract EUIID UIID { get; }

    public virtual UIFactory.PanelConfig PanelConfig { get; set; }

    public GameObject GameObject => gameObject;

    public List<UIVisual> Visuals { get; private set; } = new List<UIVisual>();

    /// <summary>
    /// 是否已经执行过Awake
    /// </summary>
    private bool _awaked = false;

    /// <summary>
    /// 是否已经执行过Started
    /// </summary>
    private bool _started = false;

    //添加额外组件
    protected bool Regist(UIVisual visual)
    {
        if (visual == null || Visuals.Contains(visual))
        {
            return false;
        }

        Visuals.Add(visual);
        visual.MainPanel = this;
        if (_awaked)
        {
            visual.Awake();
        }

        if (_started)
        {
            visual.Start();
        }

        return true;
    }

    protected bool Unregist(UIVisual visual)
    {
        return Visuals.Remove(visual);
    }

    public abstract void OnCreate();

    public virtual void OnPanelOpen(PanelParam param = null)
    {
    }

    public virtual void BeforePlayDisplayAnimation()
    {
    }

    public virtual IEnumerator PlayCustomDisplayAnimation()
    {
        yield return null;
    }

    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }

    public virtual void BeforePlayHideAnimation()
    {
    }

    public virtual IEnumerator PlayCustomHideAnimation()
    {
        yield return null;
    }

    public virtual void OnPanelClosed()
    {
    }

    public virtual void Close()
    {
        UIFactory.Instance.Pop(UIID);
    }

    float lastTime;

    private void Update()
    {
        if (!enabled)
        {
            return;
        }

        try
        {
            OnUpdate();
            foreach (IVisual visual in Visuals)
            {
                try
                {
                    visual.OnUpdate();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
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
                foreach (IVisual visual in Visuals)
                {
                    try
                    {
                        visual.OnSecond();
                    }
                    catch (Exception e)
                    {
                        FConsole.WriteException(e);
                    }
                }
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }
    }

    private void LateUpdate()
    {
        if (!enabled)
        {
            return;
        }

        try
        {
            OnLateUpdate();
            foreach (IVisual visual in Visuals)
            {
                try
                {
                    visual.OnLateUpdate();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }
    }

    private void FixedUpdate()
    {
        if (!enabled)
        {
            return;
        }

        try
        {
            OnFixedUpdate();
            foreach (IVisual visual in Visuals)
            {
                try
                {
                    visual.OnFixedUpdate();
                }
                catch (Exception e)
                {
                    FConsole.WriteException(e);
                }
            }
        }
        catch (Exception e)
        {
            FConsole.WriteException(e);
        }
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnLateUpdate()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnSecond()
    {
    }

    public virtual void OnDestroy()
    {
        foreach (IVisual visual in Visuals)
        {
            try
            {
                visual.OnDestroy();
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }
    }

    public virtual void Awake()
    {
        _awaked = true;
        foreach (IVisual visual in Visuals)
        {
            try
            {
                visual.Awake();
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }
    }

    public virtual void Start()
    {
        _started = true;
        foreach (IVisual visual in Visuals)
        {
            try
            {
                visual.Start();
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }
    }

    public virtual void OnEnable()
    {
        foreach (IVisual visual in Visuals)
        {
            try
            {
                visual.OnEnable();
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }
    }

    public virtual void OnDisable()
    {
        foreach (IVisual visual in Visuals)
        {
            try
            {
                visual.OnDisable();
            }
            catch (Exception e)
            {
                FConsole.WriteException(e);
            }
        }
    }
}

/// <summary>
/// 拥有Panel生命周期的基类
/// </summary>
public interface IVisual : IUpdate
{
    void OnCreate();

    void OnPanelOpen(PanelParam param = null);

    void OnShow();

    void OnHide();

    void OnPanelClosed();

    void OnSecond();

    void OnDestroy();

    void Awake();

    void Start();

    void OnEnable();

    void OnDisable();
}

/// <summary>
/// 拥有Panel生命周期的基类
/// </summary>
public abstract class UIVisual : IVisual
{
    // private bool _enable = true;
    //
    // public bool enabled
    // {
    //     get { return _enable; }
    //
    //     set
    //     {
    //         if (_enable != value)
    //         {
    //             if (value)
    //             {
    //                 OnEnable();
    //             }
    //             else
    //             {
    //                 OnDisable();
    //             }
    //
    //             _enable = value;
    //         }
    //     }
    // }

    public UIBasePanel MainPanel { get; set; }

    public abstract void OnCreate();

    public virtual void OnPanelOpen(PanelParam param = null)
    {
    }

    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }

    public virtual void OnPanelClosed()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnLateUpdate()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnSecond()
    {
    }

    public virtual void OnDestroy()
    {
    }

    public virtual void Awake()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }
}