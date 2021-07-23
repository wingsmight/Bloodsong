using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;

public static class SerializedPropertyExt
{
    private delegate FieldInfo GetFieldInfoAndStaticTypeFromProperty(SerializedProperty aProperty, out Type aType);
    private static GetFieldInfoAndStaticTypeFromProperty m_GetFieldInfoAndStaticTypeFromProperty;

    public static FieldInfo GetFieldInfoAndStaticType(this SerializedProperty prop, out Type type)
    {
        if (m_GetFieldInfoAndStaticTypeFromProperty == null)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in assembly.GetTypes())
                {
                    if (t.Name == "ScriptAttributeUtility")
                    {
                        MethodInfo mi = t.GetMethod("GetFieldInfoAndStaticTypeFromProperty", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        m_GetFieldInfoAndStaticTypeFromProperty = (GetFieldInfoAndStaticTypeFromProperty)Delegate.CreateDelegate(typeof(GetFieldInfoAndStaticTypeFromProperty), mi);
                        break;
                    }
                }
                if (m_GetFieldInfoAndStaticTypeFromProperty != null) break;
            }
            if (m_GetFieldInfoAndStaticTypeFromProperty == null)
            {
                UnityEngine.Debug.LogError("GetFieldInfoAndStaticType::Reflection failed!");
                type = null;
                return null;
            }
        }
        return m_GetFieldInfoAndStaticTypeFromProperty(prop, out type);
    }

    public static T GetCustomAttributeFromProperty<T>(this SerializedProperty prop) where T : System.Attribute
    {
        var info = prop.GetFieldInfoAndStaticType(out _);
        return info.GetCustomAttribute<T>();
    }

    public static string GetObjectReferenceValueName(this SerializedProperty property)
    {
        if (property.objectReferenceValue == null)
        {
            return "";
        }
        else
        {
            return property.objectReferenceValue.name;
        }
    }
    public static T GetValue<T>(this SerializedProperty property) where T : class
    {
        object obj = property.serializedObject.targetObject;
        string path = property.propertyPath.Replace(".Array.data", "");
        string[] fieldStructure = path.Split('.');
        Regex rgx = new Regex(@"\[\d+\]");
        for (int i = 0; i < fieldStructure.Length; i++)
        {
            if (fieldStructure[i].Contains("["))
            {
                int index = System.Convert.ToInt32(new string(fieldStructure[i].Where(c => char.IsDigit(c)).ToArray()));
                obj = GetFieldValueWithIndex(rgx.Replace(fieldStructure[i], ""), obj, index);
            }
            else
            {
                obj = GetFieldValue(fieldStructure[i], obj);
            }
        }
        return (T)obj;
    }

    public static bool SetValue<T>(this SerializedProperty property, T value) where T : class
    {
        object obj = property.serializedObject.targetObject;
        string path = property.propertyPath.Replace(".Array.data", "");
        string[] fieldStructure = path.Split('.');
        Regex rgx = new Regex(@"\[\d+\]");
        for (int i = 0; i < fieldStructure.Length - 1; i++)
        {
            if (fieldStructure[i].Contains("["))
            {
                int index = System.Convert.ToInt32(new string(fieldStructure[i].Where(c => char.IsDigit(c)).ToArray()));
                obj = GetFieldValueWithIndex(rgx.Replace(fieldStructure[i], ""), obj, index);
            }
            else
            {
                obj = GetFieldValue(fieldStructure[i], obj);
            }
        }

        string fieldName = fieldStructure.Last();
        if (fieldName.Contains("["))
        {
            int index = System.Convert.ToInt32(new string(fieldName.Where(c => char.IsDigit(c)).ToArray()));
            return SetFieldValueWithIndex(rgx.Replace(fieldName, ""), obj, index, value);
        }
        else
        {
            //Debug.Log(value);
            return SetFieldValue(fieldName, obj, value);
        }
    }

    private static object GetFieldValue(string fieldName, object obj, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        FieldInfo field = obj.GetType().GetField(fieldName, bindings);
        if (field != null)
        {
            return field.GetValue(obj);
        }
        return default(object);
    }

    private static object GetFieldValueWithIndex(string fieldName, object obj, int index, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        FieldInfo field = obj.GetType().GetField(fieldName, bindings);
        if (field != null)
        {
            object list = field.GetValue(obj);
            if (list.GetType().IsArray)
            {
                return ((object[])list)[index];
            }
            else if (list is IEnumerable)
            {
                return ((IList)list)[index];
            }
        }
        return default(object);
    }

    public static bool SetFieldValue(string fieldName, object obj, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        FieldInfo field = obj.GetType().GetPrivateFieldRecursive(fieldName);
        if (field != null)
        {
            field.SetValue(obj, value);
            return true;
        }
        return false;
    }

    public static bool SetFieldValueWithIndex(string fieldName, object obj, int index, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        FieldInfo field = obj.GetType().GetField(fieldName, bindings);
        if (field != null)
        {
            object list = field.GetValue(obj);
            if (list.GetType().IsArray)
            {
                ((object[])list)[index] = value;
                return true;
            }
            else if (value is IEnumerable)
            {
                ((IList)list)[index] = value;
                return true;
            }
        }
        return false;
    }
}