using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Message", menuName = "Message")]
[Serializable]
public class Message : ScriptableObject
{
    [SerializeField] private LanguageMessagePrasesDictionary phrasesDict;


    [Serializable]
    public class Phrase
    {
        [TextArea(8, 20)]
        public string text;
    }


    public List<Message.Phrase> Phrases => phrasesDict[Localization.CurrentLanguage].phrases;
}
