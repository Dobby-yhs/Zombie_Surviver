using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드


// 총을 구현
public class Gun : MonoBehaviour {

    public GameObject[] guns;

    public int currentGunIndex;

    private void Start()
    {
        currentGunIndex = PlayerPrefs.GetInt("SelectedGun", 0);

        for (int i = 0; i < guns.Length; i++)
        {
            if (i == currentGunIndex)
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
        PlayerPrefs.SetInt("SelectedGun", newGunIndex);
        guns[currentGunIndex].gameObject.SetActive(true);
    }
}