using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTweener : Tweener
{
    public Vector3 StartScale = Vector3.zero;
    public Vector3 EndScale = Vector3.one;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));

    protected override void Play(float time)
    {
        transform.localScale = (EndScale - StartScale) * Curve.Evaluate(time) + StartScale;
    }
}
