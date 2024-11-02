using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFitRolling : MonoBehaviour
{
    public float DeltaX = 10;
    public float DeltaSeconds = 1;

    float waitSeconds;
    private void Awake()
    {
        waitSeconds = DeltaSeconds;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rolling();
    }

    bool left = true;

    void Rolling()
    {
        if (waitSeconds > 0)
        {
            waitSeconds -= Time.deltaTime;
            return;
        }
        if (transform.childCount == 0) return;
        float maxWidth = GetComponent<RectTransform>().rect.width;
        RectTransform childTransform = transform.GetChild(0).GetComponent<RectTransform>();
        float pivotX = childTransform.pivot.x;
        float childWidth = childTransform.rect.width;
        Vector3 lp = childTransform.anchoredPosition3D;
        if (childWidth <= maxWidth)
        {
            lp.x = 0;
            childTransform.anchoredPosition3D = lp;
            return;
        }
        float range = childWidth - maxWidth;
        if (left)
        {
            lp.x -= DeltaX * Time.deltaTime;
            float offset = Mathf.Lerp(-range, 0, pivotX);
            if (lp.x <= offset)
            {
                lp.x = offset;
                waitSeconds = DeltaSeconds;
                left = false;
            }
        }
        else
        {
            lp.x += DeltaX * Time.deltaTime;
            float offset = Mathf.Lerp(0, range, pivotX);
            if (lp.x >= offset)
            {
                lp.x = offset;
                waitSeconds = DeltaSeconds;
                left = true;
            }
        }
        childTransform.anchoredPosition3D = lp;
    }
}
