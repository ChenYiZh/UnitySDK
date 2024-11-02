using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UITweenCustom : MonoBehaviour
{
    [SerializeField]
    protected AnimationCurve _animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public AnimationCurve AnimationCurve
    {
        get
        {
            if (_animationCurve == null)
            {
                _animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            }
            return _animationCurve;
        }
        set
        {
            if (value == null || value.keys.Length < 2)
            {
                _animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            }
            else
            {
                _animationCurve = value;
            }
        }
    }
    public float Duration = 0.3f;
    public float DeltaSeconds = 0.1f;

    public virtual void Replay()
    {
        Play();
    }

#if UNITY_EDITOR
    [ContextMenu("Replay")]
    private void ReplayEditor()
    {
        Replay();
    }
    [ContextMenu("Play")]
    private void PlayEditor()
    {
        Play();
    }
#endif
    public abstract void Play();

    public abstract bool IsPlaying();

    /// <summary>
    /// 获取Component，如果有直接返回没有就添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(Transform transform) where T : Component
    {
        return GetOrAddComponent<T>(transform.gameObject);
    }
    /// <summary>
    /// 获取Component，如果有直接返回没有就添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        T t = obj.GetComponent<T>();
        if (t == null)
        {
            return obj.AddComponent<T>();
        }
        else
        {
            return t;
        }
    }
}