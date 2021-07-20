using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private Slider slider;


    private void Awake()
    {
        slider.onValueChanged.AddListener(SetVolume);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SetVolume);
    }


    public void LoadData()
    {
        slider.value = Storage.GeneralSettings.volume;
    }
    public void SaveData()
    {
        Storage.GeneralSettings.volume = AudioListener.volume;
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
