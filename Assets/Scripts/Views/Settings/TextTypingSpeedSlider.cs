using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingSpeedSlider : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private Slider slider;


    private float maxValue;


    private void Awake()
    {
        maxValue = slider.maxValue;

        slider.onValueChanged.AddListener(SetValue);
    }
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SetValue);
    }


    public void LoadData()
    {
        slider.value = maxValue - Storage.GeneralSettings.testTypingSpeed;
    }
    public void SaveData()
    {
        Storage.GeneralSettings.testTypingSpeed = TextTyping.Speed;
    }

    private void SetValue(float value)
    {
        TextTyping.Speed = maxValue - value;
    }
}
