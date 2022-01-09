using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SoSerializer : ISerializer<ScriptableObject>
{
    public void Serialize(Stream serializationStream, ScriptableObject graph)
    {
        Type gettingType = graph.GetType();
        List<FieldInfo> allFields = gettingType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).ToList();
        List<FieldInfo> variableFields = allFields.Where(field => field.HasAttribute(typeof(VariableSave))).ToList();
        List<FieldInfo> linkableFields = allFields.Where(field => field.HasAttribute(typeof(LinkableSave))).ToList();

        string wholeDataAsString = "";
        wholeDataAsString += graph.GetType().ToString() + "\n";//typeName
        wholeDataAsString += graph.name;//objectName

        wholeDataAsString += $"\n{typeof(VariableSave)}";

        foreach (FieldInfo field in variableFields)
        {
            object value = field.GetValue(graph);
            Type fieldType = field.FieldType;
            if (fieldType.IsPrimitive())
            {
                wholeDataAsString += string.Format("\n{0}|{1}", field.Name, value);
            }
            else if (fieldType.IsList())
            {
                var json = JsonConvert.SerializeObject(value);
                wholeDataAsString += string.Format("\n{0}|{1}", field.Name, json);
            }
            else
            {
                var json = JsonUtility.ToJson(value);
                wholeDataAsString += string.Format("\n{0}|{1}", field.Name, json);
            }

            if (fieldType.IsList())
            {
                field.SetValue(graph, fieldType.GetGenericArguments()[0].GetIList());
            }
            else
            {
                field.SetValue(graph, null);
            }
        }

        wholeDataAsString += $"\n{typeof(LinkableSave)}";

        foreach (FieldInfo field in linkableFields)
        {
            object value = field.GetValue(graph);
            Type fieldType = field.FieldType;
            if (fieldType.IsList())
            {
                foreach(var element in value as IEnumerable)
                {
                    if(element != null)
                    {
                        Type elementType = element.GetType();
                        wholeDataAsString += string.Format("\n{0}|{1}|{2}", field.Name, (element as ScriptableObject)?.name, elementType);
                    }
                }

                field.SetValue(graph, fieldType.GetGenericArguments()[0].GetIList());
            }
            else
            {
                wholeDataAsString += string.Format("\n{0}|{1}|{2}", field.Name, (value as ScriptableObject)?.name, fieldType);

                field.SetValue(graph, null);
            }
        }

        string encryptedDataToSave = EncryptUtility.Encrypt(wholeDataAsString);

        BinaryWriter binaryWriter = new BinaryWriter(serializationStream);
        binaryWriter.Write(encryptedDataToSave);
        binaryWriter.Flush();
        binaryWriter.Close();
    }

    public ScriptableObject Deserialize(Stream serializationStream)
    {
        using (var binaryReader = new BinaryReader(serializationStream))
        {
            string contents = EncryptUtility.Decrypt(binaryReader.ReadString());
            List<string> pairs = contents.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string typeName = pairs[0];
            pairs.RemoveAt(0);
            string objectName = pairs[0];
            pairs.RemoveAt(0);

            List<string> variablePairs = pairs.TakeWhile(x => x != typeof(LinkableSave).ToString()).ToList();
            if(variablePairs.Count > 0)
                variablePairs.RemoveAt(0);

            List<string> linkablePairs = pairs.SkipWhile(x => x != typeof(LinkableSave).ToString()).ToList();
            if(linkablePairs.Count > 0)
                linkablePairs.RemoveAt(0);

            Type gettingType = Type.GetType(typeName);
            ScriptableObject data = ScriptableObjectFinder.Get(objectName, gettingType) as ScriptableObject;

            string key, value;
            foreach (string pair in variablePairs)
            {
                string[] keyValue = pair.Split('|');
                key = keyValue[0];
                value = keyValue[1];

                FieldInfo fieldInfo = gettingType.GetField(key, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

                if (fieldInfo != null)
                {
                    Type fieldType = fieldInfo.FieldType;
                    if (fieldType.IsPrimitive())
                    {
                        fieldInfo.SetValue(data, Convert.ChangeType(value, fieldType));
                    }
                    else if (fieldType.IsList())
                    {
                        var list = JsonConvert.DeserializeObject(value, fieldInfo.FieldType);
                        fieldInfo.SetValue(data, list);
                    }
                    else
                    {
                        object newObjectValue = JsonUtility.FromJson(value, fieldInfo.FieldType);
                        fieldInfo.SetValue(data, newObjectValue);
                    }
                }
            }

            Type type;
            foreach (string pair in linkablePairs)
            {
                string[] keyValue = pair.Split('|');
                key = keyValue[0];
                value = keyValue[1];
                type = Type.GetType(keyValue[2]);

                FieldInfo fieldInfo = gettingType.GetField(key, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

                if (fieldInfo != null)
                {
                    Type fieldType = fieldInfo.FieldType;
                    if (fieldType.IsList())
                    {
                        IList fieldAsList = (IList)fieldInfo.GetValue(data);
                        Type arg = fieldInfo.FieldType.GetGenericArguments()[0];
                        ScriptableObject instance = ScriptableObjectFinder.Get(value, type) as ScriptableObject;

                        if(instance != null)
                        {
                            if (fieldAsList == null)
                            {
                                IList newList = arg.GetIList();
                                newList.Add(instance);
                                fieldInfo.SetValue(data, newList);
                            }
                            else
                            {
                                fieldAsList.Add(instance);
                                fieldInfo.SetValue(data, fieldAsList);
                            }
                        }
                    }
                    else
                    {
                        ScriptableObject instance = ScriptableObjectFinder.Get(value, type) as ScriptableObject;
                        fieldInfo.SetValue(data, instance);
                    }
                }
            }

            return data;
        }
    }
}
