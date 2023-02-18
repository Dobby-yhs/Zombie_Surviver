using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 내비메쉬 관련 코드

public class Merchant : MonoBehaviour
{
    public GameObject merchant;

    public GameObject testButton;

    private PlayerInput playerInput;

    public bool isCollide;


    private void Start()
    {
        playerInput = GameObject.FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (testButton.activeSelf == true) 
        {
            isCollide = true;
            //Debug.Log(isCollide);

            if (Input.GetKeyDown(KeyCode.V)) {
                testButton.SetActive(false);    // 상점 UI 비활성화
                // Destroy(merchant);
                merchant.SetActive(false);
                playerInput.EnableInput();

                isCollide = false;
                //Debug.Log(isCollide);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision!");
            
            testButton.SetActive(true);     // 상점 UI 활성화
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
