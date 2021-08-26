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
    }

    void Update()
    {
        
    }
}
