using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(PropertyBlockManager)), CanEditMultipleObjects]
public class PropertyBlockManagerEditor : Editor
{
    SerializedProperty affectChildren;

    SerializedProperty colors;
    ReorderableList colorsList;

    SerializedProperty floats;
    ReorderableList floatsList;

    SerializedProperty ints;
    ReorderableList intsList;

    SerializedProperty vectors4;
    ReorderableList vectors4List;

    SerializedProperty bools;
    ReorderableList boolsList;

    private void OnEnable()
    {
        affectChildren = serializedObject.FindProperty("affectChildren");

        colors = serializedObject.FindProperty("colorParams");

        colorsList = new ReorderableList(serializedObject, colors, true, true, true, true);
        colorsList.drawElementCallback = DrawColorsListItems;
        colorsList.drawHeaderCallback = DrawColorsListHeader;


        floats = serializedObject.FindProperty("floatParams");

        floatsList = new ReorderableList(serializedObject, floats, true, true, true, true);
        floatsList.drawElementCallback = DrawFloatsListItems;
        floatsList.drawHeaderCallback = DrawFloatsListHeader;


        ints = serializedObject.FindProperty("intParams");

        intsList = new ReorderableList(serializedObject, ints, true, true, true, true);
        intsList.drawElementCallback = DrawIntsListItems;
        intsList.drawHeaderCallback = DrawIntsListHeader;


        vectors4 = serializedObject.FindProperty("vectorParams");

        vectors4List = new ReorderableList(serializedObject, vectors4, true, true, true, true);
        vectors4List.drawElementCallback = DrawVectors4ListItems;
        vectors4List.drawHeaderCallback = DrawVectors4ListHeader;


        bools = serializedObject.FindProperty("boolParams");

        boolsList = new ReorderableList(serializedObject, bools, true, true, true, true);
        boolsList.drawElementCallback = DrawBoolsListItems;
        boolsList.drawHeaderCallback = DrawBoolsListHeader;
    }

    void DrawColorsListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = colorsList.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

        // Create a property field and label field for each property. 

        // The 'item1' property
        // The label field for level (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Name");

        //The property field for level. Since we do not need so much space in an int, width is set to 20, height of a single line.
        EditorGUI.PropertyField(
            new Rect(rect.x + 50, rect.y, 150, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item1"),
            GUIContent.none
        );


        // The 'item2' property
        // The label field for quantity (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x + 220, rect.y, 100, EditorGUIUtility.singleLineHeight), "Value");

        //The property field for quantity (width 20, height of a single line)
        EditorGUI.PropertyField(
            new Rect(rect.x + 270, rect.y, 50, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2"),
            GUIContent.none
        );
    }

    void DrawColorsListHeader(Rect rect)
    {
        string name = "Colors Parameters";
        EditorGUI.LabelField(rect, name);
    }

    void DrawFloatsListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = floatsList.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

        // Create a property field and label field for each property. 

        // The 'item1' property
        // The label field for level (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Name");

        //The property field for level. Since we do not need so much space in an int, width is set to 20, height of a single line.
        EditorGUI.PropertyField(
            new Rect(rect.x + 50, rect.y, 150, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item1"),
            GUIContent.none
        );


        // The 'item2' property
        // The label field for quantity (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x + 220, rect.y, 100, EditorGUIUtility.singleLineHeight), "Value");

        //The property field for quantity (width 20, height of a single line)
        EditorGUI.PropertyField(
            new Rect(rect.x + 270, rect.y, 50, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2"),
            GUIContent.none
        );
    }

    void DrawFloatsListHeader(Rect rect)
    {
        string name = "Floats Parameters";
        EditorGUI.LabelField(rect, name);
    }

    void DrawIntsListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = intsList.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

        // Create a property field and label field for each property. 

        // The 'item1' property
        // The label field for level (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Name");

        //The property field for level. Since we do not need so much space in an int, width is set to 20, height of a single line.
        EditorGUI.PropertyField(
            new Rect(rect.x + 50, rect.y, 150, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item1"),
            GUIContent.none
        );


        // The 'item2' property
        // The label field for quantity (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x + 220, rect.y, 100, EditorGUIUtility.singleLineHeight), "Value");

        //The property field for quantity (width 20, height of a single line)
        EditorGUI.PropertyField(
            new Rect(rect.x + 270, rect.y, 50, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2"),
            GUIContent.none
        );
    }

    void DrawIntsListHeader(Rect rect)
    {
        string name = "Ints Parameters";
        EditorGUI.LabelField(rect, name);
    }

    void DrawVectors4ListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = vectors4List.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

        // Create a property field and label field for each property. 

        // The 'item1' property
        // The label field for level (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Name");

        //The property field for level. Since we do not need so much space in an int, width is set to 20, height of a single line.
        EditorGUI.PropertyField(
            new Rect(rect.x + 50, rect.y, 150, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item1"),
            GUIContent.none
        );


        // The 'item2' property
        // The label field for quantity (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x + 210, rect.y, 100, EditorGUIUtility.singleLineHeight), "Value");

        EditorGUI.Vector4Field(
            new Rect(rect.x + 270, rect.y, 300, EditorGUIUtility.singleLineHeight),
            "",
            element.FindPropertyRelative("item2").vector4Value
        );

        /*//The property field for quantity (width 20, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x + 270, rect.y, 10, EditorGUIUtility.singleLineHeight), "w");
        EditorGUI.PropertyField(
            new Rect(rect.x + 280, rect.y, 20, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2.w"),
            GUIContent.none
        );

        EditorGUI.LabelField(new Rect(rect.x + 310, rect.y, 10, EditorGUIUtility.singleLineHeight), "x");
        EditorGUI.PropertyField(
            new Rect(rect.x + 330, rect.y, 20, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2.x"),
            GUIContent.none
        );

        EditorGUI.LabelField(new Rect(rect.x + 360, rect.y, 10, EditorGUIUtility.singleLineHeight), "y");
        EditorGUI.PropertyField(
            new Rect(rect.x + 380, rect.y, 20, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2.y"),
            GUIContent.none
        );

        EditorGUI.LabelField(new Rect(rect.x + 410, rect.y, 10, EditorGUIUtility.singleLineHeight), "z");
        EditorGUI.PropertyField(
            new Rect(rect.x + 430, rect.y, 20, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2.z"),
            GUIContent.none
        );*/
    }

    void DrawVectors4ListHeader(Rect rect)
    {
        string name = "Vectors4 Parameters";
        EditorGUI.LabelField(rect, name);
    }

    void DrawBoolsListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = boolsList.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

        // Create a property field and label field for each property. 

        // The 'item1' property
        // The label field for level (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Name");

        //The property field for level. Since we do not need so much space in an int, width is set to 20, height of a single line.
        EditorGUI.PropertyField(
            new Rect(rect.x + 50, rect.y, 150, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item1"),
            GUIContent.none
        );


        // The 'item2' property
        // The label field for quantity (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x + 220, rect.y, 100, EditorGUIUtility.singleLineHeight), "Value");

        //The property field for quantity (width 20, height of a single line)
        EditorGUI.PropertyField(
            new Rect(rect.x + 270, rect.y, 50, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("item2"),
            GUIContent.none
        );
    }

    void DrawBoolsListHeader(Rect rect)
    {
        string name = "Bools Parameters";
        EditorGUI.LabelField(rect, name);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(affectChildren);

        colorsList.DoLayoutList();
        floatsList.DoLayoutList();
        intsList.DoLayoutList();
        vectors4List.DoLayoutList();
        boolsList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }   
}
