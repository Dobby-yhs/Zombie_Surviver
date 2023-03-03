using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class IngameUpgrade : MonoBehaviour
{

    public PistolData pistolData;
    public RifleData rifleData;
    public SniperData sniperData;

    public int damage;
    public int magCapacity;
    public float timeBetFire;
    public float reloadTime;

    private int SelectedGun;

    public GameManager gameManager;
    private int SavedCoin = 0;

    public TMP_Text Coin;
    public TMP_Text Dmg;
    public TMP_Text Reload;
    public TMP_Text MaxMag;
    public TMP_Text BulletSpeed;

    // public GameObject UpgradeUI;
    public bool exit = false;

    public static event Action<int> IsChanged_D;
    public static event Action<int> IsChanged_M;
    public static event Action<float> IsChanged_T;
    public static event Action<float> IsChanged_R;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        SelectedGun = PlayerPrefs.GetInt("SelectedGun", 0);
              
        if (SelectedGun == 0)
        {
            damage = pistolData.damage;
            magCapacity = pistolData.magCapacity;
            timeBetFire = pistolData.timeBetFire;
            reloadTime = pistolData.reloadTime;
        }

        else if (SelectedGun == 1)
        {
            damage = rifleData.damage;
            magCapacity = rifleData.magCapacity;
            timeBetFire = rifleData.timeBetFire;
            reloadTime = rifleData.reloadTime;
        }

        else if (SelectedGun == 2)
        {
            damage = sniperData.damage;
            magCapacity = sniperData.magCapacity;
            timeBetFire = sniperData.timeBetFire;
            reloadTime = sniperData.reloadTime;
        }
    }

    private void OnEnable() {
        SavedCoin = PlayerPrefs.GetInt("SavedCoin", 0);
        // SavedCoin = gameManager.coin;
    }

    private void OnDisable() {
        PlayerPrefs.SetInt("SavedCoin", SavedCoin);
    }

    private void Update()
    {
        
        if (SelectedGun == 0)
        {
            Coin.text = SavedCoin.ToString();
            Dmg.text = pistolData.damage.ToString();
            Reload.text = pistolData.reloadTime.ToString("F2");
            MaxMag.text = pistolData.magCapacity.ToString();
            BulletSpeed.text = pistolData.timeBetFire.ToString("F2");
        }

        else if (SelectedGun == 1)
        {
            Coin.text = SavedCoin.ToString();
            Dmg.text = rifleData.damage.ToString();
            Reload.text = rifleData.reloadTime.ToString("F2");
            MaxMag.text = rifleData.magCapacity.ToString();
            BulletSpeed.text = rifleData.timeBetFire.ToString("F2");
        }

        else if (SelectedGun == 2)
        {
            Coin.text = SavedCoin.ToString();
            Dmg.text = sniperData.damage.ToString();
            Reload.text = sniperData.reloadTime.ToString("F2");
            MaxMag.text = sniperData.magCapacity.ToString();
            BulletSpeed.text = sniperData.timeBetFire.ToString("F2");
        }
    }

    public void DamageUpgrade()
    {
        if (SelectedGun == 0)
        {
            if (SavedCoin > 0)
            {
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                pistolData.damage += 2;
                damage += 2;
                IsChanged_D?.Invoke(damage);
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 1)
        {
            if (SavedCoin > 0)
            {
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                rifleData.damage += 2;
                damage += 2;
                IsChanged_D?.Invoke(damage);
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 2)
        {
            if (SavedCoin > 0)
            {
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                sniperData.damage += 2;
                damage += 2;
                IsChanged_D?.Invoke(damage);
            }
            else
            {
                return;
            }
        }
    }

    public void ReloadUpgrade()
    {
        if (SelectedGun == 0)
        {
            if (SavedCoin > 0)
            {
                if(pistolData.reloadTime > 0.1f) 
                { 
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                    pistolData.reloadTime -= 0.005f;
                    reloadTime -= 0.005f;
                    IsChanged_R?.Invoke(reloadTime);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 1)
        {
            if (SavedCoin > 0)
            {
                if (rifleData.reloadTime > 0.1f)
                {
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                    rifleData.reloadTime -= 0.005f;
                    reloadTime -= 0.005f;
                    IsChanged_R?.Invoke(reloadTime);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 2)
        {
            if (SavedCoin > 0)
            {
                if (sniperData.reloadTime > 0.1f) { 
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                    sniperData.reloadTime -= 0.005f;
                    reloadTime -= 0.005f;
                    IsChanged_R?.Invoke(reloadTime);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }


    public void MaxUpgrade()
    {
        if (SelectedGun == 0)
        {
            if (SavedCoin > 0)
            {
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                pistolData.magCapacity += 2;
                magCapacity += 2;
                IsChanged_M?.Invoke(magCapacity);
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 1)
        {
            if (SavedCoin > 0)
            {
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                rifleData.magCapacity += 2;
                magCapacity += 2;
                IsChanged_M?.Invoke(magCapacity);
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 2)
        {
            if (SavedCoin > 0)
            {
                PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                sniperData.magCapacity += 2;
                magCapacity += 2;
                IsChanged_M?.Invoke(magCapacity);
            }
            else
            {
                return;
            }
        }
    }

    public void BulletSpeedUpgrade()
    {
        if (SelectedGun == 0)
        {
            if (SavedCoin > 0)
            {
                if (pistolData.timeBetFire > 0.01f)
                {
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                    pistolData.timeBetFire -= 0.005f;
                    timeBetFire -= 0.005f;
                    IsChanged_T?.Invoke(timeBetFire);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 1)
        {
            if (SavedCoin > 0)
            {
                if (rifleData.timeBetFire > 0.01f) { 
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                    rifleData.timeBetFire -= 0.005f;
                    timeBetFire -= 0.005f;
                    IsChanged_T?.Invoke(timeBetFire);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        else if (SelectedGun == 2)
        {
            if (SavedCoin > 0)
            {
                if (sniperData.timeBetFire > 0.01f)
                {
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin -= 1);
                    sniperData.timeBetFire -= 0.005f;
                    timeBetFire -= 0.005f;
                    IsChanged_T?.Invoke(timeBetFire);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

    }

    public void ExitButton()
    {
        exit = true;    // onClick하면 exit를 true로 변경
        // SetActive를 빼고 exit 변수를 설정하여
        // Merchant.cs에서 exit의 true를 확인한 후 merchantUI를 false로 할 수 있게끔 수정
    }
}

