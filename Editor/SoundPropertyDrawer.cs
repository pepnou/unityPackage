using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Sound))]
public class SoundPropertyDrawer : PropertyDrawer
{
    Dictionary<string, int> groupNameIndexes = null;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var nameRect        = new Rect(position.x, position.y, position.width, 20);
        var groupNamesRect  = new Rect(position.x, position.y+20, position.width, 20);

        SerializedProperty nameProperty = property.FindPropertyRelative("_name");
        EditorGUI.PropertyField(nameRect, nameProperty);
        string name = nameProperty.stringValue;

        //Debug.Log(fieldInfo.GetValue(property.serializedObject.targetObject));
        //Sound target = (Sound)(fieldInfo.GetValue(property.serializedObject.targetObject));
        
        if (groupNameIndexes == null)
            groupNameIndexes = new Dictionary<string, int>();
        
        
        if(!groupNameIndexes.ContainsKey(name))
        {
            groupNameIndexes.Add(name, 0);
        }

        //AudioManager2 audioManager2 = GameObject.FindObjectOfType<AudioManager2>();
        AudioManager2 audioManager2 = (AudioManager2)property.serializedObject.targetObject;
        string[] groupNames = audioManager2.GetGroupNames_internal().ToArray();
        
        groupNameIndexes[name] = EditorGUI.Popup(groupNamesRect, groupNameIndexes[name], groupNames);

        property.FindPropertyRelative("groupName").stringValue = groupNames[groupNameIndexes[name]];



        EditorGUI.EndProperty();
        //base.OnGUI(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 45;
        //return base.GetPropertyHeight(property, label);
    }
}
