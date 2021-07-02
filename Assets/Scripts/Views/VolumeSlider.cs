using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        slider.value = audioSource.volume;
        slider.onValueChanged.AddListener(SetVolume);
    }
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SetVolume);
    }


    private void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
