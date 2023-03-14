using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Gun3;

    public GameObject rifleValue;
    public GameObject sniperValue;

    public TMP_Text Coin;
    public TMP_Text Health;
    public TMP_Text Speed;

    public Button[] menuButtonList;

    private int SavedCoin;
    private int UsedCoin;

    private float SavedHealth;
    private float SavedSpeed;
    
    public int rifleCoin = 3;
    public int sniperCoin = 20;

    private void Start()
    {
        SavedCoin = PlayerPrefs.GetInt("SavedCoin");
        UsedCoin = PlayerPrefs.GetInt("UsedCoin", 0);
        SavedHealth = PlayerPrefs.GetFloat("SavedHealth", 100f);
        SavedSpeed = PlayerPrefs.GetFloat("SavedSpeed", 5f);
        Coin.text = SavedCoin.ToString();
        Health.text = SavedHealth.ToString();
        Speed.text = SavedSpeed.ToString();
    }

    void Update()
    {
        Coin.text = SavedCoin.ToString();
        Health.text = SavedHealth.ToString();
        Speed.text = SavedSpeed.ToString("F1");
    }
   
    public void HealthUpgrade()
    {
        if (SavedCoin <= 0)
        {
            return;
        }
        PlayerPrefs.SetFloat("SavedHealth", SavedHealth += 5f);
        PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
        PlayerPrefs.SetInt("UsedCoin", UsedCoin += 1);
        Health.text = SavedHealth.ToString();
    }

    public void SpeedUpgrade()
    {
        if (SavedCoin <= 0)
        {
            return;
        }
        PlayerPrefs.SetFloat("SavedSpeed", SavedSpeed += 0.1f);
        PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
        PlayerPrefs.SetInt("UsedCoin", UsedCoin += 1);
        Speed.text = SavedSpeed.ToString("F1");
    }

    public void SelectedButton(int currentButton)
    {
        for (int i = 0; i < menuButtonList.Length; i++)
        {
            menuButtonList[i].interactable = true;
            if (currentButton == 0)
            {
                menuButtonList[currentButton].interactable = false;
            }
            else if (currentButton == 1 && SavedCoin >= rifleCoin)
            {
                menuButtonList[currentButton].interactable = false;
                rifleValue.SetActive(false);
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 3);
                PlayerPrefs.SetInt("UsedCoin", UsedCoin += 3);
                rifleCoin = 0;
                currentButton = 1;
            }
            else if (currentButton == 2 && SavedCoin >= sniperCoin)
            {
                menuButtonList[currentButton].interactable = false;
                rifleValue.SetActive(false);
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 20);
                PlayerPrefs.SetInt("UsedCoin", UsedCoin += 20);
                sniperCoin = 0;
                currentButton = 2;
            }
        }

        PlayerPrefs.SetInt("SelectedGun", currentButton);
    }
}
