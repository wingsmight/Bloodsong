using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        slider.onValueChanged.AddListener(SetVolume);

        LoadData();
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SetVolume);

        SaveData();
    }


    public void LoadData()
    {
        slider.value = Storage.GeneralSettings.volume;
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
