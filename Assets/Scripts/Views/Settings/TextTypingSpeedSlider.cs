using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingSpeedSlider : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextTyping textTyping;


    private float maxValue;


    private void Awake()
    {
        maxValue = slider.maxValue;

        slider.onValueChanged.AddListener(SetValue);

        LoadData();
    }
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SetValue);

        SaveData();
    }


    public void LoadData()
    {
        slider.value = maxValue - Storage.GeneralSettings.testTypingSpeed;
    }
    public void SaveData()
    {
        Storage.GeneralSettings.testTypingSpeed = textTyping.Speed;
    }

    private void SetValue(float value)
    {
        textTyping.Speed = maxValue - value;
    }
}
