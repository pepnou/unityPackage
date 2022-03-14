using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonEventSubscriber))]
public class ButtonEventSubscriberEditor : Editor
{
    SerializedProperty loadType;
    SerializedProperty scenePath;
    SerializedProperty sceneBuildIndex;
    SerializedProperty sceneOffset;
    SerializedProperty wait;
    SerializedProperty waitTime;
    SerializedProperty animate;
    SerializedProperty animator;
    SerializedProperty triggerName;
    private void OnEnable()
    {
        loadType = serializedObject.FindProperty("loadType");
        scenePath = serializedObject.FindProperty("scenePath");
        sceneBuildIndex = serializedObject.FindProperty("sceneBuildIndex");
        sceneOffset = serializedObject.FindProperty("sceneOffset");
        wait = serializedObject.FindProperty("wait");
        waitTime = serializedObject.FindProperty("waitTime");
        animate = serializedObject.FindProperty("animate");
        animator = serializedObject.FindProperty("animator");
        triggerName = serializedObject.FindProperty("triggerName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(loadType);


        EditorGUI.indentLevel = 1;
        switch (loadType.enumValueIndex)
        {
            case (int)LoadType.SCENE_NAME:
                EditorGUILayout.PropertyField(scenePath);
                if(UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + scenePath.stringValue + ".unity") == -1)
                {
                    EditorGUILayout.LabelField("Scene does not exist.");
                }
                break;
            case (int)LoadType.SCENE_BUILD_INDEX:
                EditorGUILayout.PropertyField(sceneBuildIndex);
                if (sceneBuildIndex.intValue < 0 || sceneBuildIndex.intValue >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
                {
                    EditorGUILayout.LabelField("Scene does not exist.");
                }
                break;
            case (int)LoadType.SCENE_OFFSET:
                EditorGUILayout.PropertyField(sceneOffset);
                break;
        }
        EditorGUI.indentLevel = 0;

        EditorGUILayout.PropertyField(wait);
        if (wait.boolValue)
        {
            EditorGUI.indentLevel = 1;
            EditorGUILayout.PropertyField(waitTime);
            EditorGUI.indentLevel = 0;
        }

        EditorGUILayout.PropertyField(animate);
        if (animate.boolValue)
        {
            EditorGUI.indentLevel = 1;
            EditorGUILayout.PropertyField(animator);
            EditorGUILayout.PropertyField(triggerName);
            EditorGUI.indentLevel = 0;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
