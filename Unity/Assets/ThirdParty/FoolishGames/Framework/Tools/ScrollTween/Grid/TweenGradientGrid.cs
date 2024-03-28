//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//    public class TweenGradientGrid : UITweenGrid
//    {
//        protected override void OnReplay()
//        {
//            base.OnReplay();
//            foreach (UITweenGrid.TweenItem item in _items)
//            {
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
//            UIWidget widget = item.gameObject.GetComponent<UIWidget>();
//            widget.alpha = rate;
//        }
//    }