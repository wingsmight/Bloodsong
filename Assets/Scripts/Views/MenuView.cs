using System.Collections;
using System.Collections.Generic;
using StoryMenu;
using UnityEngine;

public class MenuView : MonoBehaviour, IShowable, IHidable
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [Space(12)]
    [SerializeField] private PauseButton pauseButton;
    [SerializeField] private SaveButton saveButton;
    [SerializeField] private GameDayOrder gameDayOrder;


    private void Start()
    {
        Show();
    }


    public void Show()
    {
        fadeAnimation.Appear();

        saveButton.gameObject.SetActive(gameDayOrder.IsRunning);
    }
    public void Hide()
    {
        fadeAnimation.Disappear();

        pauseButton.Show();
    }
}
