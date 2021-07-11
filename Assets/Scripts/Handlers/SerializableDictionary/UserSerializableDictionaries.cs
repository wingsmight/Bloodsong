using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> { }

[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> { }

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> { }

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> { }

[Serializable]
public class MyClass
{
    public int i;
    public string str;
}

[Serializable]
public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass> { }

[Serializable]
public class Phrases
{
    public List<Phrase> phrases;
}

[Serializable]
public class LanguagePrasesDictionary : SerializableDictionary<Language, Phrases> { }

[Serializable]
public class MessagePhrases
{
    public List<Message.Phrase> phrases;
}

[Serializable]
public class LanguageMessagePrasesDictionary : SerializableDictionary<Language, MessagePhrases> { }


[Serializable]
public class LanguageGraphDictionary : SerializableDictionary<Language, DialogueGraphData> { }