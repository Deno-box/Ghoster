using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _rect, SerializedProperty _property, GUIContent _label)
    {
        try
        {
            int pos = int.Parse(_property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(_rect, _property, new GUIContent(((NamedArrayAttribute)attribute).names[pos]));
        }
        catch
        {
            EditorGUI.PropertyField(_rect, _property, _label);
        }
    }
}