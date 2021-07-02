using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IDataSaving
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        slider.value = Storage.GeneralSettings.volume;
        slider.onValueChanged.AddListener(SetVolume);
    }
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SetVolume);
    }


    public void SaveData()
    {
        Storage.GeneralSettings.volume = audioSource.volume;
    }

    private void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
