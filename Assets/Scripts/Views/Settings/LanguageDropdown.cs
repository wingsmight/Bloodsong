using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using UnityEngine;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;


    private void Awake()
    {
        dropdown.onValueChanged.AddListener(SetValueByIndex);
    }
    private void Start()
    {
        dropdown.SetValueWithoutNotify(GetValueIndex());
    }
    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(SetValueByIndex);
    }


    private void SetValueByIndex(int index)
    {
        var selectedLanguage = dropdown.options[index].text.ToLower();

        string languageISO = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .FirstOrDefault(x =>
                x.NativeName.ToLower().Contains(selectedLanguage.ToString())
            ).Name.Substring(0, 2);

        var newLanguageISO = Enum.GetNames(typeof(Language)).FirstOrDefault(x => x.Substring(0, 2) == languageISO);

        Localization.ChangeLanguage((Language)Enum.Parse(typeof(Language), newLanguageISO));
    }
    private int GetValueIndex()
    {
        string shortCurrentISO = Localization.CurrentLanguage.ToString().Substring(0, 2);
        string currentLanguage = new CultureInfo(shortCurrentISO).NativeName;

        return dropdown.options.FindIndex(x => x.text.ToLower().Contains(currentLanguage.ToLower()));
    }
}
