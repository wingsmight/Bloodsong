using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Message", menuName = "Message")]
[Serializable]
public class Message : ScriptableObject
{

    public List<Message.Phrase> phrases;


    [Serializable]
    public class Phrase
    {
        [TextArea(8, 20)]
        public string text;
    }
}
