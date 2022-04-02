using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    private SerializedProperty globalVolume;
    private SerializedProperty soundsBank;

    //private List<string> VolumeGroupSounds;

    //int index = 0;

    public void OnEnable()
    {
        globalVolume = serializedObject.FindProperty("globalVolume");
        soundsBank = serializedObject.FindProperty("soundsBank");
    }

    private void OnValidate()
    {
        
    }

    public override void OnInspectorGUI()
    {
        /*VolumeGroupSounds = new List<string>();

        for (int i = 0; i < soundsBank.arraySize; i++)
        {
            SerializedProperty soundsBankRef = soundsBank.GetArrayElementAtIndex(i);
            VolumeGroupSounds.Add(soundsBankRef.FindPropertyRelative("_name").stringValue);
        }*/

        DrawDefaultInspector();

        //EditorGUILayout.PropertyField(globalVolume);
        //EditorGUILayout.PropertyField(soundsBank);

        //index = EditorGUILayout.Popup(index, VolumeGroupSounds.ToArray());

        //serializedObject.ApplyModifiedProperties();
    }
}