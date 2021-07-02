using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsView : MonoBehaviour, IShowable, IHidable
{
    [SerializeField] private List<SaveSlotButton> slotViews;
    [SerializeField] private FadeAnimation fadeAnimation;


    public void Show()
    {
        List<SaveSlot> slots = new List<SaveSlot>();
        for (int i = 0; i < slotViews.Count; i++)
        {
            slots.Add(new SaveSlot(slotViews[i].GameSlot));
        }

        Show(slots);
    }
    public void Show(List<SaveSlot> slots)
    {
        for (int i = 0; i < slotViews.Count && i < slots.Count; i++)
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
