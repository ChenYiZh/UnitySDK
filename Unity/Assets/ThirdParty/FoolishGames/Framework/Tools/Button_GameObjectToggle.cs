using System;
using System.Collections;
using System.Collections.Generic;
using FoolishGames.Attribute;
using UnityEngine;

public enum EButtonToggleMethod
{
    Visible,
    Invisible
}

public class Button_GameObjectToggle : MonoBehaviour
{
    /// <summary>
    /// 被控制显示的物体
    /// </summary>
    public GameObject target;

    /// <summary>
    /// 播放音效的Id
    /// </summary>
    public int audioId = 0;

    /// <summary>
    /// 只做什么操作
    /// </summary>
    [EnumFlags] public EButtonToggleMethod methods =
        (EButtonToggleMethod)(1 << (int)EButtonToggleMethod.Visible | 1 << (int)EButtonToggleMethod.Invisible);

    private void Awake()
    {
        Util.BindClick(gameObject, audioId, () =>
        {
            if (target == null)
            {
                return;
            }

            if (((int)methods & 1 << (int)EButtonToggleMethod.Visible) > 0)
            {
                if (!target.activeSelf)
                {
                    target.SetActive(true);
                }
            }

            if (((int)methods & 1 << (int)EButtonToggleMethod.Invisible) > 0)
            {
                if (target.activeSelf)
                {
                    target.SetActive(false);
                }
            }
        });
    }
}