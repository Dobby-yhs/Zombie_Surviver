using System;   // for Action
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public GameObject merchant;

    public GameObject merchantUI;

    public bool uiSpawned = true;

    private PlayerInput playerInput;

    public static event Action<bool> IsCollideChanged;

    public bool isCollide = false;


    private void Start()
    {
        playerInput = GameObject.FindObjectOfType<PlayerInput>();
        Debug.Log("uiSpawned : " +uiSpawned);
        
    }

    private void Update()
    {
        Debug.Log("isCollide : "+isCollide);

        merchantUI.SetActive(false);
        Debug.Log("uiSpawned : " +uiSpawned);
        if (merchantUI.activeSelf == true) 
        {
            
            isCollide = true;
            Debug.Log("isCollide");
            Debug.Log("isCollide : "+isCollide);
            IsCollideChanged?.Invoke(isCollide);

            

            if (Input.GetKeyDown(KeyCode.V)) {
                merchantUI.SetActive(false);    // 상점 UI 비활성화
                // Destroy(merchant);
                merchant.SetActive(false);
                playerInput.EnableInput();

                isCollide = false;
                IsCollideChanged?.Invoke(isCollide);
                //Debug.Log(isCollide);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision!");

            merchantUI.SetActive(true);     // 상점 UI 활성화
            playerInput.DisableInput();
        }        
    }

    // private void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         isCollide = false;
    //         Debug.Log(isCollide);
    //     }
    // }

    public void Active()
    {
        merchant.SetActive(true);
    }
}