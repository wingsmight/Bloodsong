using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryView : MonoBehaviour, IShowable, IHidable
{
    [SerializeField] private FadeAnimation fadeAnimation;


    public void Show()
    {
        fadeAnimation.Appear();
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
}
