using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
    private const string PLAYER_PREFS_LANG_KEY = "Language";
    private const string DICT_FOLDER_NAME = "Languages";


    public delegate void OnLanguageChangedHandler(Language newLanguage);
    public static event OnLanguageChangedHandler OnLanguageChanged;

    private static Language currentLanguage;
    private static Dictionary<string, string> localizedDictionary = null;


    [RuntimeInitializeOnLoadMethod]
    private static void Awake()
    {
        LoadCurrentLanguage();
    }


    public static string GetValue(string key)
    {
        if (localizedDictionary == null)
        {
            LoadCurrentLanguage();
        }

        if (localizedDictionary.ContainsKey(key))
        {
            return localizedDictionary[key];
        }
        else
        {
            throw new Exception("Localized text with key \"" + key + "\" not found");
        }
    }
    public static string GetCountingValue(int count, string key)
    {
        if (count == 1)
        {
            return GetValue(key);
        }

        switch (currentLanguage)
        {
            case Language.ru_RU:
                {
                    return RussianPluralizer.Pluralize(count, key);
                }
            case Language.en_US:
                {
                    return EnglishPluralizer.Pluralize(count, key);
                }
            default:
                {
                    return GetValue(key);
                }
        }
    }
    public static void ChangeLanguage(Language newLanguage)
    {
        currentLanguage = newLanguage;
        PlayerPrefs.SetString(PLAYER_PREFS_LANG_KEY, newLanguage.ToString());

        LoadlocalizatedDictionary();

        OnLanguageChanged?.Invoke(currentLanguage);
    }

    private static void LoadCurrentLanguage()
    {
        if (!PlayerPrefs.HasKey(PLAYER_PREFS_LANG_KEY))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                currentLanguage = Language.ru_RU;
            }
            else
            {
                currentLanguage = Language.en_US;
            }
        }
        else
        {
            currentLanguage = (Language)Enum.Parse(typeof(Language), PlayerPrefs.GetString(PLAYER_PREFS_LANG_KEY));
        }

        ChangeLanguage(currentLanguage);
    }
    private static void LoadlocalizatedDictionary()
    {
        string path = Path.Combine(Application.streamingAssetsPath, DICT_FOLDER_NAME, currentLanguage + ".json");
        string dataAsJson;

        LocalizationData loadedData;
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(path);
            while (!reader.isDone) { }

            dataAsJson = reader.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(path);
        }

        loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        localizedDictionary = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedDictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
    }


    public static Language CurrentLanguage => currentLanguage;
}

public enum Language
{
    en_US,
    ru_RU,
}
