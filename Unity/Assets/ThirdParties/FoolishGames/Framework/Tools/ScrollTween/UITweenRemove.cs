﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UITweenRemove<T> : UITweenRemove where T : UITweenRemove<T>
{
    public static T Get(Transform item)
    {
        return Get(item.gameObject);
    }
    public static T Get(GameObject item)
    {
        return GetOrAddComponent<T>(item);
    }
}

public abstract class UITweenRemove : UITweenCustom
{
    public System.Action onFinish;

    private float _time;
    protected int _running;

    public virtual void ResetValues()
    {
        if (Duration < 0.0001f) Duration = 1.0f;
        if (AnimationCurve == null || AnimationCurve.length == 0) AnimationCurve = new AnimationCurve(new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 1, 0));
    }

    public override void Replay()
    {
        //Debug.LogError("Replay");
        ResetValues();
        RefreshItems();
        _time = 0;
        _running = 1;
        Animate(0);
    }

    public override void Play()
    {
        //Debug.LogError("Play");
        ResetValues();
        RefreshItems();
        _running = 1;
    }

    private void Update()
    {
        if (_running > 0)
        {
            _time += Time.deltaTime;
            float rate = Mathf.Clamp01(_time / Duration);
            Animate(AnimationCurve.Evaluate(rate));
            if (rate >= 1)
            {
                _running = 0;
                if (onFinish != null) onFinish();
            }
        }
    }

    private void RefreshItems()
    {
        foreach (Transform item in transform.parent)
        {
            GameObject obj = item.gameObject;
            if (obj.GetComponent<UITweenRemove>())
            {
                obj.GetComponent<UITweenRemove>().enabled = false;
            }
        }
        enabled = true;
    }

    private void OnDisable()
    {
        if (_running > 0)
        {
            Animate(1);
        }
        _running = 0;
    }

    public override bool IsPlaying()
    {
        return _running > 0;
    }

    protected abstract void Animate(float rate);

    public static UITweenRemove Get<T>(GameObject item) where T : UITweenRemove
    {
        return GetOrAddComponent<T>(item);
    }
}