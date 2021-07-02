using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypingSpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextTyping textTyping;


    private float maxValue;


    private void Awake()
    {
        maxValue = slider.maxValue;

        slider.value = maxValue - textTyping.Speed;
        slider.onValueChanged.AddListener(SetVolume);
    }
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SetVolume);
    }


    private void SetVolume(float value)
    {
        textTyping.Speed = maxValue - value;
    }
}
