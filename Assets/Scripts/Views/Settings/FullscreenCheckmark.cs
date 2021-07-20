using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenCheckmark : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private Toggle toggle;


    private void Awake()
    {
        toggle.onValueChanged.AddListener(SetFullscreen);
    }
    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(SetFullscreen);
    }


    public void LoadData()
    {
        toggle.isOn = Storage.GeneralSettings.isFullscreen;
    }
    public void SaveData()
    {
        Storage.GeneralSettings.isFullscreen = Screen.fullScreen;
    }

    private void SetFullscreen(bool isEnable)
    {
        Screen.fullScreen = isEnable;
    }
}
