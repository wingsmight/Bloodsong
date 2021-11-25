using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    private const char X_SYMBOL = '×';
    private const int DEFAULT_INDEX = 0;


    [SerializeField] private TMP_Dropdown dropdown;


    private void Awake()
    {
        var userResolutionOption = new TMP_Dropdown.OptionData(Display.main.systemWidth.ToString() + X_SYMBOL + Display.main.systemHeight);
        if (!dropdown.options.Any(x => x.text == userResolutionOption.text))
        {
            dropdown.options.Insert(DEFAULT_INDEX, userResolutionOption);
        }

        dropdown.onValueChanged.AddListener(SetValueByIndex);
        dropdown.value = GetValueIndex();

        LoadData();
    }
    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(SetValueByIndex);
    }


    private void SaveData()
    {
        Storage.GeneralSettings.resolution = new int[] { Screen.width, Screen.height };
    }
    private void LoadData()
    {
        if (Storage.GeneralSettings.resolution != null && Storage.GeneralSettings.resolution.Length == 2)
        {
            Screen.SetResolution(Storage.GeneralSettings.resolution[0], Storage.GeneralSettings.resolution[1],
                Screen.fullScreen);
        }
        else
        {
            Storage.GeneralSettings.resolution = new int[] { Display.main.systemWidth, Display.main.systemHeight };
        }
        dropdown.value = GetValueIndex();
    }

    private void SetValueByIndex(int index)
    {
        var option = dropdown.options[index];
        var sizeRect = option.text.ExtractNumbers();
        Screen.SetResolution(sizeRect[0], sizeRect[1], Screen.fullScreen);

        SaveData();
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
