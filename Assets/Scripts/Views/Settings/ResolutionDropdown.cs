using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour, IDataSaving
{
    [SerializeField] private TMP_Dropdown dropdown;


    private void Awake()
    {
        dropdown.SetValueWithoutNotify(GetValueIndex());
        dropdown.onValueChanged.AddListener(SetValueByIndex);
    }
    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(SetValueByIndex);
    }


    public void SaveData()
    {
        Storage.GeneralSettings.resolution = new int[] { Screen.width, Screen.height };
    }

    private void SetValueByIndex(int index)
    {
        var option = dropdown.options[index];
        var sizeRect = option.text.ExtractNumbers();
        Screen.SetResolution(sizeRect[0], sizeRect[1], Screen.fullScreen);
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

        return 0;// TODO
    }
}
