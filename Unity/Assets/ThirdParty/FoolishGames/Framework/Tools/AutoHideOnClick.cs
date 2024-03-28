using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoHideOnClick : MonoBehaviour
{
    private bool isTouchDown = false;

    // Update is called once per frame
    void Update()
    {
        if (Util.IsTouching())
        {
            isTouchDown = true;
        }

        if (isTouchDown && !Util.IsTouching())
        {
            isTouchDown = false;
            gameObject.SetActive(false);
        }
    }
}