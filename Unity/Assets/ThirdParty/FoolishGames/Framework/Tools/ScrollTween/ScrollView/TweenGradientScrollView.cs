using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenGradientScrollView : TweenItemScrollView
{
    protected override void InitializeItem(Transform item)
    {
        //UIWidget widget = item.gameObject.GetComponent<UIWidget>();
        //if (widget == null)
        //{
        //    bool active = item.gameObject.activeSelf;
        //    widget = item.gameObject.AddComponent<UIWidget>();
        //    item.gameObject.SetActive(false);
        //    item.gameObject.SetActive(active);
        //}
        CanvasGroup widget = Util.GetOrAddComponent<CanvasGroup>(item);
        widget.alpha = 0;
    }

    //protected override void OnDisableItem(Transform item)
    //{
    //    UIWidget widget = item.gameObject.GetComponent<UIWidget>();
    //    if (widget != null)
    //    {
    //        widget.alpha = 1;
    //    }
    //}

    protected override void UpdateItem(Transform item, float rate)
    {
        CanvasGroup widget = GetOrAddComponent<CanvasGroup>(item);
        widget.alpha = rate;
    }

    protected override void ResetItem(Transform item)
    {
        CanvasGroup widget = GetOrAddComponent<CanvasGroup>(item);
        widget.alpha = 1;
    }
}