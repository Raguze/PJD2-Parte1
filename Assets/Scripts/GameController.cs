using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController player;
    private InputController input;
    private void Awake() 
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        input = GameObject.FindObjectOfType<InputController>();
        input.Init();

        input.OnDirection.AddListener(player.SetDirectionalInput);
        input.OnJumpButtonDown.AddListener(player.OnJumpInputDown);
        input.OnJumpButtonUp.AddListener(player.OnJumpInputUp);

        Pistol pistolPrefab = Resources.Load<Pistol>("PlayerWeapons/Pistol");
        WeaponDTO pistolDTO = Resources.Load<WeaponDTO>("DTO/Pistol");
        Weapon pistol = Instantiate<Weapon>(pistolPrefab);
        pistol.Init(pistolDTO);
        player.AddWeapon(pistol);
    }

    void Update()
    {
        
    }
}
