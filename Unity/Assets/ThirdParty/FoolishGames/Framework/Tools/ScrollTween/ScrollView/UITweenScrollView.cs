using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UITweenScrollView : UITweenCustom
{
    public CScrollView ScrollView;

    public bool AutoRegist = true;

    private int _limit;

    protected abstract void Initialize();
    protected virtual void Awake()
    {
        _limit = 0;
        if (ScrollView == null && gameObject.GetComponent<CScrollView>())
        {
            ScrollView = gameObject.GetComponent<CScrollView>();
            if (AutoRegist)
            {
                ScrollView.AfterReposition.AddListener(OnReposition);
            }
        }
    }

    protected void OnReposition()
    {
        if (ScrollView.Limit != _limit)
        {
            _limit = ScrollView.Limit;
            Initialize();
        }
        Play();
    }

    protected virtual void Update()
    {

    }
}