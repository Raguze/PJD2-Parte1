using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneController))]
public class SceneControllerEditor : Editor
{
    SerializedProperty prefabs;
    private void OnEnable() {
        prefabs = serializedObject.FindProperty("prefabs");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        EditorGUILayout.LabelField("Actions");
        
        if(GUILayout.Button("Button")) {
            Debug.Log((prefabs.serializedObject.targetObject as SceneController).prefabs[0]);
        }
        EditorGUILayout.PropertyField(prefabs);
        
    }
}

