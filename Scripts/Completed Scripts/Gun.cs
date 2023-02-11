using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject[] guns;
    
    public int currentGunIndex = 0;

    private void Start() {
        for ( int i = 0; i < guns.Length; i++)
        {
            if ( i == currentGunIndex) 
            {
                guns[i].gameObject.SetActive(true);
            }
            else
            {
                guns[i].gameObject.SetActive(false);
            }
        }
    }

    public void SwitchGun(int newGunIndex)
    {
        guns[currentGunIndex].gameObject.SetActive(false);
        currentGunIndex = newGunIndex;
        guns[currentGunIndex].gameObject.SetActive(true);
    }

    /*
    private void OnDisable() {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        // currentGun.gameObject.SetActive(false);
        guns[currentGunIndex].gameObject.SetActive(false);

    }
    */
}
