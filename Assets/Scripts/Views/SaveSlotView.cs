using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaveSlotView : MonoBehaviour
{
    private const string TEST_SPRITE_PATH = "Backgrounds/VillageFarView";
    private const string SLOT_NAME = "Слот";

    private readonly Color emptyBackgroundImageColor = new Color(0.45f, 0.45f, 0.45f);
    private readonly Color backgroundImageColor = Color.white;


    [SerializeField] private int slotNumber;

    [SerializeField] private Image locationImage;
    [SerializeField] private TextMeshProUGUI lastOpenDateTextView;
    [SerializeField] private TextMeshProUGUI slotNumberTextView;


    private Sprite testLocationSprite;


    private void Awake()
    {
        var texture = Resources.Load<Texture2D>(TEST_SPRITE_PATH);
        testLocationSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
    }
    public void Show(SaveSlot saveSlot)
    {
        if (!saveSlot.IsEmpty)
        {
            locationImage.sprite = testLocationSprite;
            locationImage.color = backgroundImageColor;
            lastOpenDateTextView.text = saveSlot.lastOpenDate.ToShortDateString();
            slotNumberTextView.text = "";
        }
        else
        {
            locationImage.sprite = null;
            locationImage.color = emptyBackgroundImageColor;
            lastOpenDateTextView.text = "";
            slotNumberTextView.text = SLOT_NAME + " " + slotNumber;
        }
    }
}
