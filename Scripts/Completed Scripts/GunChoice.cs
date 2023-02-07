using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChoice : MonoBehaviour
{ 
    public GameObject pistolObject, rifleObject, sniperObject;
    public Button pistolButton, rifleButton, sniperButton;

    public PistolData pistolData;
    public RifleData riflleData;
    public SniperData sniperData;
    
    public Gun gun;

    void Start() {
        pistolButton.onClick.AddListener(pistolChoice);
        rifleButton.onClick.AddListener(rifleChoice);
        sniperButton.onClick.AddListener(sniperChoice);        
    }

    void pistolChoice() {
        Debug.Log("Pistol");

        gun.currentGunIndex = 0;
        gun.SwitchGun(gun.currentGunIndex);

        Debug.Log(gun.currentGunIndex);

        pistolData.damage += 5;
        Debug.Log(pistolData.damage);
    }

    void rifleChoice() {
        Debug.Log("Rifle");
       
        gun.currentGunIndex = 1;
        gun.SwitchGun(gun.currentGunIndex);

        Debug.Log(gun.currentGunIndex);

        pistolObject.SetActive(false);
        rifleObject.SetActive(true);
        sniperObject.SetActive(false);
    }


    void sniperChoice() {
        Debug.Log("Sniper");

        gun.currentGunIndex = 2;
        gun.SwitchGun(gun.currentGunIndex);

        Debug.Log(gun.currentGunIndex);

        pistolObject.SetActive(false);
        rifleObject.SetActive(false);
        sniperObject.SetActive(true);
    }
}
