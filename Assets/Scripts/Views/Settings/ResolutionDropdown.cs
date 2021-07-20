using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour, IDataSaving, IDataLoading
{
    private const int DEFAULT_INDEX = 0;


    [SerializeField] private TMP_Dropdown dropdown;


    private void Awake()
    {
        var userResolutionOption = new TMP_Dropdown.OptionData(Screen.currentResolution.width + "×" + Screen.currentResolution.height);
        if (dropdown.options.Contains(userResolutionOption))
        {
            dropdown.options.Insert(DEFAULT_INDEX, userResolutionOption);
        }

        dropdown.onValueChanged.AddListener(SetValueByIndex);
        dropdown.value = GetValueIndex();
    }
    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(SetValueByIndex);
    }


    public void SaveData()
    {
        Storage.GeneralSettings.resolution = new int[] { Screen.width, Screen.height };
    }
    public void LoadData()
    {
        if (Storage.GeneralSettings.resolution != null)
        {
            Screen.SetResolution(Storage.GeneralSettings.resolution[0], Storage.GeneralSettings.resolution[1],
                Storage.GeneralSettings.isFullscreen);
        }
        dropdown.value = GetValueIndex();
    }

    private void SetValueByIndex(int index)
    {
        var option = dropdown.options[index];
        var sizeRect = option.text.ExtractNumbers();
        Screen.SetResolution(sizeRect[0], sizeRect[1], Storage.GeneralSettings.isFullscreen);
    }
    private int GetValueIndex()
    {
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            var sizeRect = dropdown.options[i].text.ExtractNumbers();
            if (sizeRect[0] == Screen.width && sizeRect[1] == Screen.height)
            {
                return i;
            }
        }

        return DEFAULT_INDEX;
    }
}
