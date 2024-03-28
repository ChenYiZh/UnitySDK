using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTweener : Tweener
{
    public Vector3 StartEuler;

    public Vector3 EndEuler;

    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));

    protected override void Play(float time)
    {
        transform.localEulerAngles = (EndEuler - StartEuler) * Curve.Evaluate(time) + StartEuler;
    }
}