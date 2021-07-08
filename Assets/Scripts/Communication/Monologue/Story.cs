using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Story", menuName = "Story")]
[Serializable]
public class Story : ScriptableObject
{
    [SerializeField] private LanguagePrasesDictionary phrasesDict;


    public List<Phrase> Phrases => phrasesDict[Localization.CurrentLanguage].phrases;
}
