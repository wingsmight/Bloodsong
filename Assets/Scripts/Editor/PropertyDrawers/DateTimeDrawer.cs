using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DateTimeData))]
public class DateTimeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var firstLineRect = new Rect(
            x: position.x - 120,
            y: position.y + 20,
            width: position.width - 4,
            height: EditorGUIUtility.singleLineHeight
        );
        DrawMainProperties(firstLineRect, property, 5.0f, new string[] { "minute", "hour", "day", "month", "year" });

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
    private void DrawMainProperties(Rect rect, SerializedProperty human, float space, string[] propList)
    {
        int count = propList.Length;
        rect.width = (rect.width - 2 * space) / count;

        Rect labelRect = rect;
        labelRect.width = 40;
        labelRect.x += rect.width - 22;
        EditorGUI.LabelField(labelRect, "hh:mm");

        rect.x += rect.width + space + 14;
        DrawProperty(rect, human.FindPropertyRelative(propList[1]));

        labelRect = rect;
        labelRect.width = 4;
        labelRect.x += rect.width;
        EditorGUI.LabelField(labelRect, ":");

        rect.x += rect.width + space;
        DrawProperty(rect, human.FindPropertyRelative(propList[0]));

        labelRect.width = rect.width;
        labelRect.x += rect.width + space;
        EditorGUI.LabelField(labelRect, "DD");

        rect.x += rect.width + space + 14;
        DrawProperty(rect, human.FindPropertyRelative(propList[2]));

        labelRect.width = rect.width;
        labelRect.x += rect.width + space + 13;
        EditorGUI.LabelField(labelRect, "MM");

        rect.x += rect.width + space + 18;
        DrawProperty(rect, human.FindPropertyRelative(propList[3]));

        labelRect.width = rect.width;
        labelRect.x += rect.width + space + 20;
        EditorGUI.LabelField(labelRect, "YY");

        rect.x += rect.width + space + 15;
        rect.width *= 2;
        DrawProperty(rect, human.FindPropertyRelative(propList[4]));
    }
    private void DrawProperty(Rect rect, SerializedProperty property)
    {
        EditorGUI.PropertyField(rect, property, GUIContent.none);
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18 * 2;
    }
}
