using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTweener : Tweener
{
    protected override void Play(float time)
    {

    }
    protected override void OnEnd()
    {
        base.OnEnd();
        gameObject.SetActive(false);
    }
}
