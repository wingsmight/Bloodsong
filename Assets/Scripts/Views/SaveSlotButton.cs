using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaveSlotButton : UIButton
{
    private const string SLOT_NAME = "Слот";

    private readonly Color emptyBackgroundImageColor = new Color(0.45f, 0.45f, 0.45f);
    private readonly Color backgroundImageColor = Color.white;


    [SerializeField] private GameSlot gameSlot;

    [SerializeField] private Image locationImage;
    [SerializeField] private TextMeshProUGUI lastOpenDateTextView;
    [SerializeField] private TextMeshProUGUI slotNumberTextView;
    [SerializeField] private GameDayOrder gameDayOrder;
    [SerializeField] private SaveSlotsView saveSlotsView;
    [SerializeField] private MenuView menuView;


    protected override void Awake()
    {
        base.Awake();
    }
    public void Show(SaveSlot saveSlot)
    {
        if (gameSlot.IsUsed)
        {
            locationImage.sprite = saveSlot.locationSprite;
            locationImage.AdjustSize();
            locationImage.color = backgroundImageColor;
            lastOpenDateTextView.text = saveSlot.lastExitDate.ToShortDateString();
            slotNumberTextView.text = "";
        }
        else
        {
            locationImage.sprite = null;
            locationImage.color = emptyBackgroundImageColor;
            lastOpenDateTextView.text = "";
            slotNumberTextView.text = SLOT_NAME + " " + (gameSlot.Index + 1).ToString();
        }
    }

    protected override void OnClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (gameSlot.Index != Storage.GeneralSettings.currentGameSlotIndex
            && Storage.GeneralSettings.currentGameSlotIndex != -1)
        {
            SaveLoadLauncher.Instance.SaveDatas();
        }

        gameSlot.IsUsed = true;
        Storage.GeneralSettings.currentGameSlotIndex = gameSlot.Index;
        Storage.GeneralSettings.lastGameSlot = gameSlot.Index;

        saveSlotsView.Hide();
        menuView.Hide();

        int phraseIndex = Storage.GetData<GameDayData>().phraseIndex;
        gameDayOrder.StartDay(phraseIndex);
    }


    public GameSlot GameSlot => gameSlot;
}