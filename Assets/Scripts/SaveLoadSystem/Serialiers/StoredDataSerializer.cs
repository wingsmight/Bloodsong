using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class StoredDataSerializer : ISerializer<IStoredData>
{
    public void Serialize(Stream serializationStream, IStoredData storedData)
    {
        string wholeDataAsString = "";
        wholeDataAsString += storedData.GetType().ToString() + "\n";
        wholeDataAsString += JsonUtility.ToJson(storedData);

        string encryptedDataToSave = EncryptUtility.Encrypt(wholeDataAsString);

        BinaryWriter binaryWriter = new BinaryWriter(serializationStream);
        binaryWriter.Write(encryptedDataToSave);
        binaryWriter.Flush();
        binaryWriter.Close();
    }

    public IStoredData Deserialize(Stream serializationStream)
    {
        using (var binaryReader = new BinaryReader(serializationStream))
        {
            string content = EncryptUtility.Decrypt(binaryReader.ReadString());

            string typeName = content.Substring(0, content.IndexOf('\n'));
            Type objectType = Type.GetType(typeName);

            content = content.Substring(content.IndexOf('\n'));

            return JsonUtility.FromJson(content, objectType) as IStoredData;
        }
    }
}
