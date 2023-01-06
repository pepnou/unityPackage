using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Group))]
public class GroupPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        //EditorGUI.PropertyField(position, property, label);
    }
}
