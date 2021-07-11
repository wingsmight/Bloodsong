using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StatAdditive))]
public class StatAdditiveDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        rect = EditorGUI.PrefixLabel(rect, label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        DrawMainProperties(rect, property, 5.0f, new string[] { "type", "value", });

        EditorGUI.indentLevel = indent;
    }
    private void DrawMainProperties(Rect rect, SerializedProperty human, float space, string[] propList)
    {
        int count = propList.Length;
        rect.width = (rect.width - 2 * space) / count;
        rect.x = 25;

        rect.x += rect.width + space;
        EditorGUI.PropertyField(rect, human.FindPropertyRelative(propList[0]), GUIContent.none);

        rect.x += rect.width + space;
        EditorGUI.PropertyField(rect, human.FindPropertyRelative(propList[1]), GUIContent.none);
    }
}
