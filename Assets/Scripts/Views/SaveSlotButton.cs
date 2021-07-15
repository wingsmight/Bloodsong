using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SaveSlotButton : UIButton
{
    private const string SLOT_NAME = "Slot";

    private readonly Color emptyBackgroundImageColor = new Color(0.45f, 0.45f, 0.45f);
    private readonly Color backgroundImageColor = Color.white;


    [SerializeField] private GameSlot gameSlot;

    [SerializeField] private Image locationImage;
    [SerializeField] private TextMeshProUGUI lastOpenDateTextView;
    [SerializeField] private TextMeshProUGUI slotNumberTextView;
    [SerializeField] private GameDayControl gameDay;
    [SerializeField] private SaveSlotsView saveSlotsView;
    [SerializeField] private MenuView menuView;
    [SerializeField] private GameObject emptyOverlay;
    [SerializeField] private Button deleteButton;


    protected override void Awake()
    {
        base.Awake();

        gameSlot.OnDeleted += ShowEmpty;
    }


    public void Show(SaveSlot saveSlot)
    {
        if (gameSlot.IsUsed)
        {
            locationImage.sprite = saveSlot.locationSprite;
            locationImage.color = locationImage.sprite == null ? emptyBackgroundImageColor : Color.white;
            locationImage.AdjustSize(); // just add Aspect ratio filter
            if (saveSlot.lastExitDate == DateTime.MinValue) // TODO kostttt
            {
                lastOpenDateTextView.text = DateTime.Now.ToShortDateString();
            }
            else
            {
                lastOpenDateTextView.text = saveSlot.lastExitDate.ToShortDateString();
            }
            slotNumberTextView.text = "";
            emptyOverlay.SetActive(false);
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            ShowEmpty();
        }
    }

    protected override void OnClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        // if (gameSlot.Index != Storage.GeneralSettings.currentGameSlotIndex
        //     && Storage.GeneralSettings.currentGameSlotIndex != -1)
        // {
        //     SaveLoadLauncher.Instance.SaveDatas();
        // }

        gameSlot.IsUsed = true;
        Storage.GeneralSettings.currentGameSlotIndex = gameSlot.Index;
        Storage.GeneralSettings.lastGameSlot = gameSlot.Index;

        SaveLoadLauncher.Instance.LoadDatas();
        SaveLoadLauncher.Instance.LinkDatas();

        saveSlotsView.Hide();
        menuView.Hide();

        gameDay.StartDay();
    }
    private void ShowEmpty()
    {
        locationImage.sprite = null;
        locationImage.color = emptyBackgroundImageColor;
        lastOpenDateTextView.text = "";
        slotNumberTextView.text = Localization.GetValue(SLOT_NAME) + " " + (gameSlot.Index + 1).ToString();
        emptyOverlay.SetActive(true);
        deleteButton.gameObject.SetActive(false);
    }


    public GameSlot GameSlot => gameSlot;
}
