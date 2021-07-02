using System;
using System.Collections.Generic;
using UnityEngine;

public static class StringExt
{
    public static string ReplaceAt(this string input, int index, char newChar)
    {
        if (input == null)
        {
            throw new ArgumentNullException("input");
        }
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
    public static List<int> AllIndexesOf(this string str, string value)
    {
        if (String.IsNullOrEmpty(value))
            throw new ArgumentException("the string to find may not be empty", "value");
        List<int> indexes = new List<int>();
        for (int index = 0; ; index += value.Length)
        {
            index = str.IndexOf(value, index);
            if (index == -1)
                return indexes;
            indexes.Add(index);
        }
    }
    public static string AddColorTag(this string text, Color color)
    {
        string colorHex = ColorUtility.ToHtmlStringRGB(color);

        return "<color=#" + colorHex + ">" + text + "</color>";
    }
}