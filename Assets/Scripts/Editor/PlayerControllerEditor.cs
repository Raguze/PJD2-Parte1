using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public PlayerController player;
    private void OnEnable() {
        player = target as PlayerController;
    }
    
    BaseSave ForEach(Weapon w)
    {
       return w.Serialize();
    }

    void HandleSaveWeapons()
    {
        List<BaseSave> saves = new List<BaseSave>();
        player.GetWeapons().ForEach((Weapon w) => {
            BaseSave bs = w.Serialize();
            saves.Add(bs);
        });
        string json = JsonUtility.ToJson(saves[0],true);
        Debug.Log(json);
        string fullPath = Application.dataPath + "/Resources/Saves/SaveWeapon1.json";
        Debug.Log(fullPath);
        File.WriteAllText(fullPath,json);

        // {
        //     "Name": "Revolver",
        //     "Ammo": 6,
        //     "AmmoMax": 6,
        //     "Damage": 12,
        //     "FireRate": 1.5,
        //     "ReloadSpeed": 2.0,
        //     "BulletSpeed": 5.0,
        //     "Distance": 5.0,
        //     "Type": 1
        // }
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("Save Weapons"))
        {
            HandleSaveWeapons();
        }

        if(GUILayout.Button("Load Weapons"))
        {
            HandleLoadWeapons();
        }
    }

    private void HandleLoadWeapons()
    {
        TextAsset jsonString = Resources.Load<TextAsset>("Saves/SaveWeapon1");
        WeaponSave save = JsonUtility.FromJson<WeaponSave>(jsonString.text);
        player.GetWeapons()[0].Deserialize(save);
    }
}
