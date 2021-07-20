using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;


    private void Awake()
    {
        slider.onValueChanged.AddListener(SetVolume);

        LoadData();
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SetVolume);
    }


    private void LoadData()
    {
        slider.value = Storage.GeneralSettings.volume;
    }
    private void SaveData()
    {
        Storage.GeneralSettings.volume = AudioListener.volume;
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;

        SaveData();
    }
}
