using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorObject : MonoBehaviour
{
    /// <summary>
    /// 启动移除还是隐藏
    /// </summary>
    public bool bDestroy = true;

    private void Awake()
    {
        gameObject.SetActive(false);
        if (bDestroy)
        {
            GameObject.Destroy(gameObject);
        }
    }
}