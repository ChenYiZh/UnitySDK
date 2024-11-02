using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaTweener : Tweener
{
    public float StartAlpha = 0;
    public float EndAlpha = 1;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));

    Image image = null;
    Text text = null;
    CanvasGroup canvasGroup = null;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        text = GetComponent<Text>();
    }

    protected override void Play(float time)
    {
        float alpha = (EndAlpha - StartAlpha) * Curve.Evaluate(time) + StartAlpha;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
            return;
        }
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
        if (text != null)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}
