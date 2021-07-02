using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;


    private void Awake()
    {
        dropdown.SetValueWithoutNotify(GetResolutionIndex());
        dropdown.onValueChanged.AddListener(SetResolutionByIndex);
    }
    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(SetResolutionByIndex);
    }


    private void SetResolutionByIndex(int index)
    {
        var option = dropdown.options[index];
        var sizeRect = option.text.ExtractNumbers();
        Screen.SetResolution(sizeRect[0], sizeRect[1], Screen.fullScreen);
    }
    private int GetResolutionIndex()
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
