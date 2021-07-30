using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour, IHidable, IResetable, IDataLoading, IDataSaving
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private Image image;
    [SerializeField] private Color emptryColor;


    public event Action OnShown;


    public void Show(Location location)
    {
        StartCoroutine(ShowRoutine(location));
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
    public void HideImmediately()
    {
        fadeAnimation.SetVisible(false);
    }
    public void Reset()
    {
        HideImmediately();
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
            Storage.GeneralSettings.CurrentSaveSlot.lastLocation = new Location(Storage.GetData<GameDayData>().locationName);
        }
    }

    private void SetLocation(Location location)
    {
        image.sprite = location.Sprite;
        image.color = image.sprite == null ? emptryColor : Color.white;
    }
    private IEnumerator ShowRoutine(Location location)
    {
        Hide();

        yield return new WaitWhile(() => fadeAnimation.IsShowing);

        fadeAnimation.Appear();
        SetLocation(location);

        yield return new WaitWhile(() => !fadeAnimation.IsShowing);

        OnShown?.Invoke();
    }
}
