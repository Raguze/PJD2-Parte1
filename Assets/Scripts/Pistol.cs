using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pistol : Weapon
{
    protected override void Awake()
    {
        Type = WeaponType.Pistol;
        base.Awake();
    }
}
