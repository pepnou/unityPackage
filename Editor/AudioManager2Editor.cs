using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(AudioManager2))]
public class AudioManager2Editor : Editor
{
    private SerializedProperty globalVolume;
    private SerializedProperty soundsBank;

    private SerializedProperty soundsToStart;
    private SerializedProperty soundsToStop;

    //private SerializedProperty groups;

    //private List<string> VolumeGroupSounds;

    //int index = 0;

    public void OnEnable()
    {
        globalVolume = serializedObject.FindProperty("globalVolume");
        soundsBank = serializedObject.FindProperty("soundsBank");

        soundsToStart = serializedObject.FindProperty("soundsToStart");
        soundsToStop = serializedObject.FindProperty("soundsToStop");

        //groups = serializedObject.FindProperty("groups");
    }

    private void OnValidate()
    {

    }

    public override void OnInspectorGUI()
    {
        // AudioManager2 AM = (AudioManager2)target;
        // List<string> test = AM.GetGroupNames_internal();
        // List<string> test = AudioManager2.GetGroupNames();
        // 
        // foreach (var item in test)
        // {
        //     EditorGUILayout.LabelField(item);
        // }


        // serializedObject.Update();
        // 
        // 
        // /*VolumeGroupSounds = new List<string>();
        // 
        // for (int i = 0; i < soundsBank.arraySize; i++)
        // {
        //     SerializedProperty soundsBankRef = soundsBank.GetArrayElementAtIndex(i);
        //     VolumeGroupSounds.Add(soundsBankRef.FindPropertyRelative("_name").stringValue);
        // }*/
        // 
        // //DrawDefaultInspector();
        // 
        // EditorGUILayout.PropertyField(globalVolume);
        // EditorGUILayout.PropertyField(soundsBank);
        // 
        // EditorGUILayout.PropertyField(soundsToStart);
        // EditorGUILayout.PropertyField(soundsToStop);
        // 
        // //EditorGUILayout.PropertyField(groups);
        // 
        // //index = EditorGUILayout.Popup(index, VolumeGroupSounds.ToArray());
        // 
        // serializedObject.ApplyModifiedProperties();

        DrawDefaultInspector();
    }
}