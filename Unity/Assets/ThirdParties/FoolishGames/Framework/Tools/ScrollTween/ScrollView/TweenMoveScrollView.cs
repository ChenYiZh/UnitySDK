using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMoveScrollView : TweenLineScrollView
{
    private float _deltaPos;
    //[SerializeField]
    private bool _isVertical;

    private Vector2 _startPos;

    protected override void OnInitialize()
    {
        _startPos = ScrollView.StartPosition;
        _isVertical = ScrollView.IsVertical;
        _deltaPos = (ScrollView.IsVertical ? ScrollView.ItemSize.x : ScrollView.ItemSize.y) * ScrollView.Limit;
        _deltaPos /= 2.0f;
    }

    //protected override void OnDisableItem(Transform item, int line, int index)
    //{
    //    UpdateItem(item, line, index, 1);
    //}

    protected override void InitializeItem(Transform item)
    {
        //CanvasGroup widget = item.gameObject.GetComponent<CanvasGroup>();
        //if (widget == null)
        //{
        //    bool active = item.gameObject.activeSelf;
        //    widget = item.gameObject.AddComponent<CanvasGroup>();
        //    item.gameObject.SetActive(false);
        //    item.gameObject.SetActive(active);
        //}
        CanvasGroup widget = Util.GetOrAddComponent<CanvasGroup>(item);
        widget.alpha = 0;
    }

    protected override void UpdateItem(Transform item, int line, int index, float rate)
    {
        RectTransform rect = item.GetComponent<RectTransform>();
        float delta = _deltaPos * (1 - rate);
        float sPos = _isVertical ? _startPos.x : _startPos.y;
        float deltaPos = _isVertical ? ScrollView.ItemSize.x : ScrollView.ItemSize.y;
        Vector2 v = rect.anchoredPosition;
        float pos = sPos + deltaPos * index + delta;
        rect.anchoredPosition = new Vector2(_isVertical ? pos : v.x, _isVertical ? v.y : pos);
        CanvasGroup widget = item.gameObject.GetComponent<CanvasGroup>();
        widget.alpha = rate;
    }

    protected override void ResetItem(Transform item, int line, int index)
    {
        RectTransform rect = item.GetComponent<RectTransform>();
        float sPos = _isVertical ? _startPos.x : _startPos.y;
        float deltaPos = _isVertical ? ScrollView.ItemSize.x : ScrollView.ItemSize.y;
        Vector2 v = rect.anchoredPosition;
        float pos = sPos + _deltaPos * index;
        rect.anchoredPosition = new Vector2(_isVertical ? pos : v.x, _isVertical ? v.y : pos);
        CanvasGroup widget = item.gameObject.GetComponent<CanvasGroup>();
        widget.alpha = 1;
    }
}