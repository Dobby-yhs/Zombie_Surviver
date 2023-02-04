// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class GunManager : MonoBehaviour
// {
//     public static bool isChangeWeapon = false;

//     [SerializeField]
//     static GGun[] gun;
//     static GGun[] rifle;

//     static Dictionary<string, GGun> gunDictionary = new Dictionary<string, GGun>();
//     static Dictionary<string, GGun> rifleDictionary = new Dictionary<string, GGun>();

//     public static Transform currentWeapon;

//     [SerializeField]
//     static Gun theGunController;
//     static Rifle therifleController;

//     void Start() {
//         for (int i = 0; i < gun.Length; i++)
//         {
//             gunDictionary.Add(gun[i].gunName, gun[i]);
//         }
//         for (int i = 0; i < rifle.Length; i++)
//         {
//             rifleDictionary.Add(rifle[i].gunName, rifle[i]);
//         }
//     }

//     // public static void WeaponChange(string _name)
//     // {
//     //     if(_name == "Gun") {
//     //         theGunController.GunChange(gunDictionary[_name]);
//     //     }
//     //     else if(_name == "Rifle") {
//     //         therifleController.GunChange(rifleDictionary[_name]);
//     //     }
//     // }
// }
