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

    private int SelectedGun;
    private int SavedCoin;

    public TMP_Text Coin;
    public TMP_Text Dmg;
    public TMP_Text Reload;
    public TMP_Text MaxMag;
    public TMP_Text BulletSpeed;

    // public GameObject UpgradeUI;
    public bool exit = false;

    void Start()
    {
        SelectedGun = PlayerPrefs.GetInt("SelectedGun", 0);
        SavedCoin = PlayerPrefs.GetInt("SavedCoin");
    }

    void Update()
    {
        Coin.text = SavedCoin.ToString();
        if (SelectedGun == 0)
        {
            Dmg.text = pistolData.damage.ToString();
            Reload.text = pistolData.reloadTime.ToString("F1");
            MaxMag.text = pistolData.magCapacity.ToString();
            BulletSpeed.text = pistolData.timeBetFire.ToString("F2");
        }

        else if (SelectedGun == 1)
        {
            Dmg.text = rifleData.damage.ToString();
            Reload.text = rifleData.reloadTime.ToString("F1");
            MaxMag.text = rifleData.magCapacity.ToString();
            BulletSpeed.text = rifleData.timeBetFire.ToString("F2");
        }

        else if (SelectedGun == 2)
        {
            Dmg.text = sniperData.damage.ToString();
            Reload.text = sniperData.reloadTime.ToString("F1");
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
                PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                pistolData.damage += 5;
                
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
                PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                rifleData.damage += 5;
                
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
                PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                sniperData.damage += 5;
                
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
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                    pistolData.reloadTime -= 0.1f;
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
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                    rifleData.reloadTime -= 0.1f;
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
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                    sniperData.reloadTime -= 0.1f;
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
                PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                pistolData.magCapacity += 5;
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
                PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                rifleData.magCapacity += 5;
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
                PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                sniperData.magCapacity += 5;
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
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                    pistolData.timeBetFire -= 0.01f;
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
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                    rifleData.timeBetFire -= 0.01f;
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
                    PlayerPrefs.SetInt("SavedCoin", SavedCoin - 1);
                    sniperData.timeBetFire -= 0.01f;
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

