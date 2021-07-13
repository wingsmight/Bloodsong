using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private Image image;
    [SerializeField] private Color emptryColor;


    public void Show()
    {
        fadeAnimation.Appear();
    }
    public void Show(Location location)
    {
        Show();

        image.sprite = location.Sprite;
        image.color = image.sprite == null ? emptryColor : Color.white;
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
        Show(new Location(Storage.GetData<GameDayData>().locationName));
    }
    public void SaveData()
    {
        if (image.sprite != null)
        {
            Storage.GetData<GameDayData>().locationName = image.sprite.name; // TODO may produce error if something is wrong with path/name
        }
    }
}
