using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;  // 무기 교체 중복 실행 방지. (True면 못하게)

    [SerializeField]
    private GGG[] guns;  // 모든 종류의 총을 원소로 가지는 배열

    // 관리 차원에서 이름으로 쉽게 무기 접근이 가능하도록 Dictionary 자료 구조 사용.
    private Dictionary<string, GGG> gunDictionary = new Dictionary<string, GGG>();

    public static Transform currentWeapon;  // 현재 무기. static으로 선언하여 여러 스크립트에서 클래스 이름으로 바로 접근할 수 있게 함.

    [SerializeField]
    private GGGCCCC theGunController;  // 총 일땐 📜GunController.cs 활성화, 손일 땐 📜GunController.cs 비활성화 
   
    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
    }

    void Update()   // UI와 연계하여 수정 필요
    {
        if(!isChangeWeapon)
        {
            
            if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 누르면 '맨손'으로 무기 교체 실행
            {
                StartCoroutine(ChangeWeaponCoroutine("HAND", "맨손"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2 누르면 '서브 머신건'으로 무기 교체 실행
            {
                StartCoroutine(ChangeWeaponCoroutine("GUN", "SubMachineGun1"));
            }

        
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _name)
    {
        isChangeWeapon = true;

        WeaponChange(_name);

        isChangeWeapon = false;
    }

    private void WeaponChange(string _name)
    {
        theGunController.GunChange(gunDictionary[_name]);
    }
}
