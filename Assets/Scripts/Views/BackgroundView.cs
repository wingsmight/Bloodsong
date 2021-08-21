using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour, IHidable, IResetable, IDataLoading, IDataSaving
{
    private const float HIDE_DURATION = 1.0f;


    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private Image image;
    [SerializeField] private Color emptryColor;
    [Space(12)]
    [SerializeField] private StoryMenu.BacktrackOnPrevMessageButton backtrackButton;


    public event Action OnShown;


    public void Show(Location location)
    {
        StartCoroutine(ShowRoutine(location));
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
        backtrackButton.DisableAction(HIDE_DURATION);
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

        backtrackButton.EnableAction();

        OnShown?.Invoke();
    }
}
