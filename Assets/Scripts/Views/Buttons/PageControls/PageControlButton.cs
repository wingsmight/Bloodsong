using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class PageControlButton : UIButton
{
    [SerializeField] [RequireInterface(typeof(IShowPaging))] private Object textShowing;


    public virtual void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }


    protected IShowPaging TextShowing => textShowing as IShowPaging;
}
