using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpeed : MonoBehaviour, IItem
{
    public Gun gun;
    public PistolData pistolData;
    public RifleData rifleData;
    public SniperData sniperData;    

    public void Use(GameObject target) {
        switch (gun.currentGunIndex)
        {
            case 0 :
                pistolData.timeBetFire *= 4/5;
                break;
            case 1 :
                rifleData.timeBetFire *= 4/5;
                break;
            case 2 :
                sniperData.timeBetFire *= 4/5;
                break;

        }
        Debug.Log("Gun speed Up!");
        Destroy(gameObject);
    }
}
