using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public enum WeaponType { None, Pistol, Shotgun, MachineGun, Sniper, RocketLauncher }

[System.Serializable]
public class Weapon : SerializableObject
{
    [SerializeField]
    public string Name { get; protected set; }
    public int Ammo { get; protected set; }
    public int AmmoMax { get; protected set; }
    public int Damage { get; protected set; }
    public float FireRate { get; protected set; }
    public float ReloadSpeed { get; protected set; }
    public float BulletSpeed { get; protected set; }
    public float Distance { get; protected set; }
    public WeaponType Type { get; protected set; }

    [SerializeField]
    public WeaponDTO weaponDTO;

    protected bool isFiring;
    protected bool isReloading;
    protected bool CanFire 
    { 
        get {
            return !isFiring && !isReloading && Ammo > 0;
        } 
    }

    [SerializeField]
    protected Transform bulletRespawn;

    public override BaseSave Serialize()
    {
        WeaponSave save = new WeaponSave() {
            Ammo = this.Ammo,
            AmmoMax = this.AmmoMax,
            BulletSpeed = this.BulletSpeed,
            Damage = this.Damage,
            Distance = this.Distance,
            FireRate  = this.FireRate,
            Name = this.Name,
            ReloadSpeed = this.ReloadSpeed,
            Type = this.Type,
        };
        return save;
    }

    public override void Deserialize(BaseSave save)
    {
        WeaponSave ws = save as WeaponSave; 
        Name = ws.Name;
        Ammo = ws.Ammo;
        AmmoMax = ws.AmmoMax;
        Damage = ws.Damage;
        FireRate = ws.FireRate;
        ReloadSpeed = ws.ReloadSpeed;
        BulletSpeed = ws.BulletSpeed;
        Distance = ws.Distance;
    }

    protected virtual void Awake()
    {
        bulletRespawn = transform.Find("BulletRespawn");

        if(weaponDTO != null)
        {
            Init(weaponDTO);
        }
    }

    public virtual void Init(WeaponDTO wdto)
    {
        Name = wdto.Name;
        Ammo = wdto.Ammo;
        AmmoMax = wdto.AmmoMax;
        Damage = wdto.Damage;
        FireRate = wdto.FireRate;
        ReloadSpeed = wdto.ReloadSpeed;
        BulletSpeed = wdto.BulletSpeed;
        Distance = wdto.Distance;

        weaponDTO = wdto;
    }

    public void Fire()
    {
        if(CanFire)
        {
            CreateProjectile();
            StartCoroutine(FireCooldown());
        }
    }

    protected virtual void CreateProjectile()
    {
        GameObject go = Factory.GetObject("bullet", bulletRespawn.position, bulletRespawn.rotation);
        BulletController bullet = go.GetComponent<BulletController>();
        bullet.Init(weaponDTO);
    }

    private IEnumerator FireCooldown()
    {
        isFiring = true;
        Ammo--;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax);
        yield return new WaitForSeconds(FireRate);
        isFiring = false;
    }

    public void Reload()
    {
        if(!isReloading && Ammo < AmmoMax)
        {
            StartCoroutine(ReloadCooldown());
        }
    }

    private IEnumerator ReloadCooldown()
    {
        Debug.Log("Begin Reload");
        isReloading = true;
        GameEvents.WeaponReloadEvent.Invoke(ReloadSpeed);
        yield return new WaitForSeconds(ReloadSpeed);
        Ammo = AmmoMax;
        isReloading = false;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax);
        Debug.Log("End Reload");
    }

}
