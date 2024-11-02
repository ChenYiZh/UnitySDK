using FoolishGames.Attribute;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Tweeners : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler,
    ISelectHandler, IDeselectHandler
{
    /// <summary>
    /// 动画列表
    /// </summary>
    [SerializeField, ReadOnly] private Tweener[] _tweeners;

    // /// <summary>
    // /// 按钮是否是启用状态
    // /// </summary>
    // [SerializeField, ReadOnly] private bool _buttonEnabled = true;

    /// <summary>
    /// 按钮是否被覆盖者
    /// </summary>
    [SerializeField, ReadOnly] private bool _buttonHovered = false;

    /// <summary>
    /// 按钮是否在按下状态
    /// </summary>
    [SerializeField, ReadOnly] private bool _buttonPressed = false;

    private void Awake()
    {
        _tweeners = GetComponents<Tweener>();
    }

    private void OnEnable()
    {
        RefreshState();
    }

    private void OnDisable()
    {
        _buttonHovered = false;
        _buttonPressed = false;
        //RefreshState();
    }

    public void RefreshState()
    {
        if (_tweeners == null || _tweeners.Length == 0)
        {
            return;
        }

        // if (gameObjectNormal != null)
        // {
        //     gameObjectNormal.SetActive(false);
        // }
        //
        // if (gameObjectHovered != null)
        // {
        //     gameObjectHovered.SetActive(false);
        // }
        //
        // if (gameObjectOnPressed != null)
        // {
        //     gameObjectOnPressed.SetActive(false);
        // }
        //
        // if (gameObjectOnDisable != null)
        // {
        //     gameObjectOnDisable.SetActive(false);
        // }
        //
        // if (!_buttonEnabled)
        // {
        //     if (gameObjectOnDisable != null)
        //     {
        //         gameObjectOnDisable.SetActive(true);
        //     }
        //     else if (gameObjectNormal != null)
        //     {
        //         gameObjectNormal.SetActive(true);
        //     }
        // }
        // else 
        if (_buttonPressed)
        {
            foreach (Tweener tweener in _tweeners)
            {
                if (!tweener.IsRevert)
                {
                    if (!tweener.IsPlaying)
                    {
                        tweener.ResetToEnd();
                    }

                    tweener.PlayBack();
                }
            }
        }
        else if (_buttonHovered)
        {
            foreach (Tweener tweener in _tweeners)
            {
                if (!tweener.IsPlaying)
                {
                    tweener.ResetToBegin();
                }

                tweener.PlayForward();
            }
        }
        else
        {
            foreach (Tweener tweener in _tweeners)
            {
                if (!tweener.IsRevert)
                {
                    if (!tweener.IsPlaying)
                    {
                        tweener.ResetToEnd();
                    }

                    tweener.PlayBack();
                }
            }
        }
    }

    // private void Update()
    // {
    //     if (button.enabled != _buttonEnabled)
    //     {
    //         _buttonEnabled = button.enabled;
    //         RefreshState();
    //     }
    // }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown();
    }

    public void OnPointerDown()
    {
        _buttonPressed = true;
        RefreshState();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Util.Toast("OnPointerUp");
        OnPointerUp();
    }
    
    public void OnPointerUp()
    {
        _buttonPressed = false;
        RefreshState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Util.Toast("OnPointerEnter");
        OnPointerEnter();
    }
    
    public void OnPointerEnter()
    {
        _buttonHovered = true;
        RefreshState();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Util.Toast("OnPointerExit");
        OnPointerExit();
    }
    
    public void OnPointerExit()
    {
        _buttonHovered = false;
        RefreshState();
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Util.Toast("OnSelect");
        OnSelect();
    }
    
    public void OnSelect()
    {
        _buttonHovered = true;
        RefreshState();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //Util.Toast("OnDeselect");
        OnDeselect();
    }
    
    public void OnDeselect()
    {
        _buttonHovered = false;
        RefreshState();
    }
}