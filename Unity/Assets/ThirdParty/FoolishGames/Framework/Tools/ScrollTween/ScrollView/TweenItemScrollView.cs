using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TweenItemScrollView : TweenLineScrollView
{
    protected override LineItem NewLineItem(float startTime)
    {
        if (_lineItems.Count > 0)
        {
            startTime = _lineItems.Last.Value.StartTime + DeltaSeconds * ScrollView.Limit;
        }
        return new Item(this, ScrollView, PlayedLine, Duration, startTime, DeltaSeconds);
    }

    private class Item : LineItem
    {
        private Dictionary<Transform, float> _itemTimes;
        private float _deltaTime;
        private float _totalDuration;

        public Item(TweenLineScrollView script, CScrollView scrollView, int line, float duration, float startTime, float deltaTime) : base(script, scrollView, line, duration, startTime)
        {
            _deltaTime = deltaTime;
            _totalDuration = deltaTime * (_items.Length - 1) + duration;
            _time = _totalDuration;
            _itemTimes = new Dictionary<Transform, float>();
            for (int i = 0; i < _items.Length; i++)
            {
                Transform item = _items[i];
                _itemTimes.Add(item, duration);
            }
        }

        protected override void UpdateItem(Transform item, int line, int index, float rate)
        {
            float itemRate = 0;
            if (Time.time >= StartTime + _deltaTime * index)
            {
                _itemTimes[item] -= Time.deltaTime;
                itemRate = Mathf.Clamp01((_duration - _itemTimes[item]) / _duration); ;
            }
            if (rate >= 1)
            {
                itemRate = 1;
            }
            base.UpdateItem(item, line, index, itemRate);
        }
    }
}