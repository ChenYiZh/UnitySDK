//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//    public class TweenMoveGrid : UITweenGrid
//    {
//        public float DeltaPosition;

//        private Dictionary<Transform, Vector2> _theItems;

//        protected override void Initialize()
//        {
//            base.Initialize();
//            DeltaPosition = 300;
//            if (Grid != null)
//            {
//                DeltaPosition = Grid.cellWidth / 2.0f;
//            }
//            if (_theItems == null)
//            {
//                _theItems = new Dictionary<Transform, Vector2>();
//            }
//        }

//        protected override void OnReplay()
//        {
//            if (IsPlaying())
//            {
//                OnEnd();
//            }
//            base.OnReplay();
//            _theItems.Clear();
//            foreach (UITweenGrid.TweenItem item in _items)
//            {
//                _theItems.Add(item.Item, item.Item.localPosition);
//                UIWidget widget = item.Item.gameObject.GetComponent<UIWidget>();
//                if (widget == null)
//                {
//                    widget = item.Item.gameObject.AddComponent<UIWidget>();
//                    bool active = item.Item.gameObject.activeSelf;
//                    item.Item.gameObject.SetActive(false);
//                    item.Item.gameObject.SetActive(active);
//                }
//                widget.alpha = 0;
//            }
//        }

//        protected override void UpdateItem(Transform item, float rate)
//        {
//            if (!_theItems.ContainsKey(item)) return;
//            UIWidget widget = item.gameObject.GetComponent<UIWidget>();
//            widget.alpha = rate;
//            item.localPosition = _theItems[item] + Vector2.right * DeltaPosition * (1 - rate);
//        }

//        protected override void OnEnd()
//        {
//            base.OnEnd();
//            _theItems.Clear();
//        }
//    }