using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingSpeedSlider : MonoBehaviour, IDataSaving
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextTyping textTyping;


    private float maxValue;


    private void Awake()
    {
        maxValue = slider.maxValue;

        slider.value = maxValue - Storage.GeneralSettings.testTypingSpeed;
        slider.onValueChanged.AddListener(SetValue);
    }
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SetValue);
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
