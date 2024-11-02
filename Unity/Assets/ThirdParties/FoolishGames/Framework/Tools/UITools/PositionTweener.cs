using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTweener : Tweener
{
    public bool IsWorld = false;

    public Vector3 StartPosition = Vector3.zero;
    public Vector3 EndPosition = Vector3.zero;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));

    RectTransform rectTransform = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    protected override void Play(float time)
    {
        if (IsWorld)
        {
            transform.position = (EndPosition - StartPosition) * Curve.Evaluate(time) + StartPosition;
        }
        else if (rectTransform != null)
        {
            rectTransform.anchoredPosition3D = (EndPosition - StartPosition) * Curve.Evaluate(time) + StartPosition;
        }
        else
        {
            transform.localPosition = (EndPosition - StartPosition) * Curve.Evaluate(time) + StartPosition;
        }
    }
}
