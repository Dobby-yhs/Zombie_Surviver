using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMagcapacity : MonoBehaviour, IItem
{
    public Gun gun;
    public PistolData pistolData;
    public RifleData rifleData;
    public SniperData sniperData;    

    public void Use(GameObject target) {
        switch (gun.currentGunIndex)
        {
            case 0 :
                pistolData.magCapacity += 5;
                break;
            case 1 :
                rifleData.magCapacity += 5;
                break;
            case 2 :
                sniperData.magCapacity += 5;
                break;

        }
        Debug.Log("magCapacity Up!");
        Destroy(gameObject);
    }
}
