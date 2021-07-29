using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeView : MonoBehaviour, IShowable, IHidable
{
    [SerializeField] private FadeAnimation fadeAnimation;


    public virtual void Show()
    {
        fadeAnimation.Appear();
    }
    public virtual void Hide()
    {
        fadeAnimation.Disappear();
    }
}
