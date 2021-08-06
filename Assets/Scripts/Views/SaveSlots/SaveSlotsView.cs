using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsView : MonoBehaviour, IShowable, IHidable
{
    [SerializeField] private List<SaveSlotButton> slotViews;
    [SerializeField] private FadeAnimation fadeAnimation;


    public void Show()
    {
        Show(Storage.GeneralSettings.SaveSlots);
    }
    public void Show(SaveSlot[] slots)
    {
        for (int i = 0; i < slotViews.Count && i < slots.Length; i++)
        {
            slotViews[i].Show(slots[i]);
        }
        fadeAnimation.Appear();
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
}
