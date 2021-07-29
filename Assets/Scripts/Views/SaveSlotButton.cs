using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SaveSlotButton : UIButton
{
    private const string GAME_SCENE_NAME = "Story";
    private const string SLOT_NAME = "Slot";

    private readonly Color emptyBackgroundImageColor = new Color(0.45f, 0.45f, 0.45f);
    private readonly Color backgroundImageColor = Color.white;


    [SerializeField] private Image locationImage;
    [SerializeField] private TextMeshProUGUI lastOpenDateTextView;
    [SerializeField] private TextMeshProUGUI slotNumberTextView;
    [SerializeField] private FadeAnimation emptyOverlay;
    [SerializeField] private FadeAnimation deleteButton;
    [Space(12)]
    [SerializeField] private int index;


    protected override void Awake()
    {
        base.Awake();
    }


    public void Show(SaveSlot saveSlot)
    {
        if (saveSlot.isUsed)
        {
            locationImage.sprite = saveSlot.lastLocation?.Sprite;
            locationImage.color = locationImage.sprite == null ? emptyBackgroundImageColor : backgroundImageColor;
            lastOpenDateTextView.text = saveSlot.lastExitDate.Date.ToShortDateString();
            slotNumberTextView.text = "";
            emptyOverlay.SetVisible(false);
            deleteButton.Appear();
        }
        else
        {
            ShowEmpty();
        }
    }
    public void ShowEmpty()
    {
        locationImage.sprite = null;
        locationImage.color = emptyBackgroundImageColor;
        lastOpenDateTextView.text = "";
        slotNumberTextView.text = Localization.GetValue(SLOT_NAME) + " " + (index + 1).ToString();
        emptyOverlay.Appear();
        deleteButton.SetVisible(false);
    }
    public void HideDeleteButton()
    {
        deleteButton.Disappear();
    }

    protected override void OnClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        Storage.GeneralSettings.SaveSlots[index].lastExitDate = new DateTimeData(DateTime.Now);
        Storage.GeneralSettings.SaveSlots[index].isUsed = true;
        Storage.GeneralSettings.currentSaveSlotIndex = index;
        Storage.GeneralSettings.lastSaveSlotIndex = index;

        SceneManager.LoadScene(GAME_SCENE_NAME);
    }


    public int Index => index;
    public bool Interactable { get => button.interactable; set => button.interactable = value; }
}
