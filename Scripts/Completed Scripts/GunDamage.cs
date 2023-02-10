using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamage : MonoBehaviour, IItem
{
    public Gun gun;
    public PistolData pistolData;
    public RifleData rifleData;
    public SniperData sniperData;    

    public void Use(GameObject target) {
        switch (gun.currentGunIndex)
        {
            case 0 :
                pistolData.damage += 5;
                break;
            case 1 :
                rifleData.damage += 5;
                break;
            case 2 :
                sniperData.damage += 5;
                break;

        }
        Debug.Log("damage Up!");
        Destroy(gameObject);
    }
}
