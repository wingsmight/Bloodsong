using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour, IDataLoading, IDataSaving
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

    public void LoadData()
    {
        Storage.GetData<GameDayData>().lastLocation = new Location(image.sprite.name);
    }
    public void SaveData()
    {
        image.sprite = Storage.GetData<GameDayData>().lastLocation.Sprite;
    }
}
