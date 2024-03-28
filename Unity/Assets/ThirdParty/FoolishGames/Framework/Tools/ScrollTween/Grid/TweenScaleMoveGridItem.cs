//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TweenScaleMoveGridItem : UITweenRemove<TweenScaleMoveGridItem>
//{
//    Vector3 _startScale;
//    Dictionary<Transform, KeyValuePair<Vector3, Vector3>> _nextItems;
//    [SerializeField, Range(0.1f, 1.0f)]
//    float _timeRate;
//    public override void Replay()
//    {
//        _startScale = transform.localScale;
//        RefreshItemList();
//        base.Replay();
//    }

//    public override void Play()
//    {
//        if (_startScale == Vector3.zero) { _startScale = transform.localScale; }
//        if (_nextItems == null) { RefreshItemList(); }
//        base.Play();
//    }

//    public override void ResetValues()
//    {
//        base.ResetValues();
//        if (_timeRate < 0.1f)
//        {
//            _timeRate = 0.65f;
//        }
//        AnimationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
//        if (Duration < 0.1f)
//        {
//            Duration = 0.5f;
//        }
//    }

//    private void RefreshItemList()
//    {
//        if (_nextItems == null)
//        {
//            _nextItems = new Dictionary<Transform, KeyValuePair<Vector3, Vector3>>();
//        }
//        else { _nextItems.Clear(); }
//        Vector3 deltaVec = Vector3.zero;
//        for (int i = transform.GetSiblingIndex() + 1; i < transform.parent.childCount; i++)
//        {
//            Transform item = transform.parent.GetChild(i);
//            if (item.gameObject.activeSelf)
//            {
//                if (deltaVec == Vector3.zero)
//                {
//                    deltaVec = item.localPosition - transform.localPosition;
//                }
//                _nextItems.Add(item, new KeyValuePair<Vector3, Vector3>(item.localPosition, deltaVec));
//            }
//        }
//    }

//    protected override void Animate(float rate)
//    {
//        transform.localScale = _startScale * Mathf.Clamp01(1 - rate / (_timeRate - 0.1f));
//        if (rate >= _timeRate)
//        {
//            foreach (KeyValuePair<Transform, KeyValuePair<Vector3, Vector3>> item in _nextItems)
//            {
//                if (item.Key != null)
//                {
//                    item.Key.localPosition = item.Value.Key - Mathf.Clamp01((rate - 0.5f) / 0.4f) * item.Value.Value;
//                }
//            }
//            if (rate >= 1)
//            {
//                transform.gameObject.SetActive(false);
//                transform.localScale = _startScale;
//#if !UNITY_EDITOR
//                    _nextItems.Clear();
//#endif
//            }
//        }
//    }

//#if UNITY_EDITOR
//    [ContextMenu("Reset")]
//    private void ResetEditor()
//    {
//        _running = 0;
//        transform.gameObject.SetActive(true);
//        transform.localScale = _startScale;
//        foreach (KeyValuePair<Transform, KeyValuePair<Vector3, Vector3>> item in _nextItems)
//        {
//            if (item.Key != null)
//            {
//                item.Key.localPosition = item.Value.Key;
//            }
//        }
//    }
//#endif
//}