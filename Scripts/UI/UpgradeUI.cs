using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public TMP_Text Coin;
    public TMP_Text Health;
    public TMP_Text Speed;
    private int SavedCoin;
    private int UsedCoin;
    private float SavedHealth;
    private float SavedSpeed;
    

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
        Speed.text = SavedSpeed.ToString("F1");
    }
}
