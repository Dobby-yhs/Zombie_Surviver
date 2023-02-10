using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReload : MonoBehaviour, IItem
{
    public Gun gun;
    public PistolData pistolData;
    public RifleData rifleData;
    public SniperData sniperData;    

    public void Use(GameObject target) {
        switch (gun.currentGunIndex)
        {
            case 0 :
                pistolData.reloadTime *= 4/5;
                break;
            case 1 :
                rifleData.reloadTime *= 4/5;
                break;
            case 2 :
                sniperData.reloadTime *= 4/5;
                break;

        }
        Debug.Log("reload speed Up!");
        Destroy(gameObject);
    }
}
