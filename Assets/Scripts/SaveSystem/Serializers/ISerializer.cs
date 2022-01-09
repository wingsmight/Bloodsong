using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ISerializer<T>
{
    void Serialize(Stream serializationStream, T storedData);
    T Deserialize(Stream serializationStream);
}
public interface ISerializer
{
    void Serialize(Stream serializationStream, object storedData);
    object Deserialize(Stream serializationStream);
}
