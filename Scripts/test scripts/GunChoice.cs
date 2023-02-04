using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunChoice : MonoBehaviour
{ 
    public Button pistolButton, rifleButton, sniperButton;

    public GameObject rifleobject;

    public GameObject gunobject;

    // public static bool isChangeWeapon = false;

     

    // [SerializeField]
    // private GGun[] gun;
    // private GGun[] rifle;

    // private Dictionary<string, GGun> gunDictionary = new Dictionary<string, GGun>();
    // private Dictionary<string, GGun> rifleDictionary = new Dictionary<string, GGun>();

    // public static Transform currentWeapon;

    // [SerializeField]
    // private Gun theGunController;
    // private Rifle therifleController;

    void Start() {
        pistolButton.onClick.AddListener(pistolChoice);
        rifleButton.onClick.AddListener(rifleChoice);
        sniperButton.onClick.AddListener(sniperChoice);
    }

    void pistolChoice() {
        Debug.Log("Pistol");
        // if(!isChangeWeapon) {
        //     GunManager.WeaponChange("Gun");
        // }
        gunobject.SetActive(true);
        rifleobject.SetActive(false);

    }

    void rifleChoice() {
        Debug.Log("Rifle");
        // if(!isChangeWeapon) {
        //     GunManager.WeaponChange("Rifle");
        // }
        gunobject.SetActive(false);
        rifleobject.SetActive(true);
    }


    void sniperChoice() {
        Debug.Log("Sniper");
    }
}
