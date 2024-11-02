using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tweener : MonoBehaviour
{
    public bool Auto = false;

    public bool OnlyPlayInCamera = false;

    public PlayType Type = PlayType.Once;

    public float Wait = 0;
    public float Duration = 1;

    /// <summary>
    /// 间隔触发
    /// </summary>
    public float DeltaTime = 0;

    private bool isVisible;

    public bool IsPlaying { get; private set; }

    private float waitTime;
    private float time;
    private bool revert;

    /// <summary>
    /// 是否是PlayBack
    /// </summary>
    public bool IsRevert
    {
        get { return revert; }
    }

    protected abstract void Play(float time);

    public virtual void PlayForward()
    {
        if (IsPlaying && revert && time > 0)
        {
            time = Duration - time;
        }

        revert = false;
        IsPlaying = true;
    }

    public virtual void PlayBack()
    {
        if (IsPlaying && !revert && time > 0)
        {
            time = Duration - time;
        }

        revert = true;
        IsPlaying = true;
    }

    protected void Update()
    {
        if (OnlyPlayInCamera && !isVisible)
        {
            return;
        }

        if (IsPlaying)
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                return;
            }

            time -= Time.deltaTime;
            float rate = Mathf.Clamp01(time / Duration);
            if (!Util.NearBy(DeltaTime, 0) && DeltaTime > 0)
            {
                float minTime = 1 / (Duration / DeltaTime); //倍数
                int scale = Mathf.FloorToInt(rate / minTime);
                rate = scale * minTime;
            }

            if (revert)
            {
                Play(rate);
            }
            else
            {
                Play(1 - rate);
            }

            if (time < 0)
            {
                OnEnd();
                return;
            }
        }
    }

    public virtual void ResetToBegin()
    {
        time = Duration;
        waitTime = Wait;
        Play(0);
    }

    public virtual void ResetToEnd()
    {
        time = Duration;
        waitTime = Wait;
        Play(1);
    }

    protected virtual void OnEnable()
    {
        if (Auto)
        {
            if (revert)
            {
                PlayBack();
            }
            else
            {
                PlayForward();
            }
        }
    }

    protected virtual void OnDisable()
    {
        if (Auto || IsPlaying)
        {
            if (revert)
            {
                ResetToBegin();
            }
            else
            {
                ResetToEnd();
            }
        }

        IsPlaying = false;
    }

    protected virtual void OnEnd()
    {
        IsPlaying = false;
        switch (Type)
        {
            case PlayType.Once: break;
            case PlayType.Loop:
            {
                if (revert)
                {
                    ResetToEnd();
                    PlayBack();
                }
                else
                {
                    ResetToBegin();
                    PlayForward();
                }
            }
                break;
            case PlayType.PingPong:
            {
                if (revert)
                {
                    ResetToBegin();
                    PlayForward();
                }
                else
                {
                    ResetToEnd();
                    PlayBack();
                }
            }
                break;
        }
    }

    public virtual void Stop()
    {
        IsPlaying = false;
    }

    public enum PlayType
    {
        Once,
        Loop,
        PingPong,
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}