using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Gun3;

    public Button[] menuButtonList;
    
    public void SelectedButton(int currentButton)
    {
        for(int i = 0; i<menuButtonList.Length; i++)
        {
            menuButtonList[i].interactable = true;
            menuButtonList[currentButton].interactable = false;
        }

        PlayerPrefs.SetInt("SelectedGun", currentButton);
    }
}
