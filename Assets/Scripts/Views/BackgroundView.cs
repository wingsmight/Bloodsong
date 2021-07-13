using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private Image image;


    public void Show()
    {
        fadeAnimation.Appear();
    }
    public void Show(Location location)
    {
        Show();

        Storage.GetData<GameDayData>().lastLocation = location;
        image.sprite = location.Sprite;
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
    public void HideImmediately()
    {
        fadeAnimation.SetVisible(false);
    }
}
