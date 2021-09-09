using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public PlayerController player;
    private void OnEnable() {
        player = target as PlayerController;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("Save Weapons"))
        {
            string json = JsonUtility.ToJson(player.GetWeapons()[0],true);
            Debug.Log(json);
        }
    }

}
