using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 내비메쉬 관련 코드

public class Merchant : MonoBehaviour
{
    public GameObject merchant;

    public GameObject testButton;

    private PlayerInput playerInput;


    private void Start()
    {
        playerInput = GameObject.FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (testButton.activeSelf == true) {
            if (Input.GetKeyDown(KeyCode.V)) {
                testButton.SetActive(false);    // 상점 UI 비활성화
                // merchant.SetActive(false);
                Destroy(merchant);
                playerInput.EnableInput();
                
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

    public void Active()
    {
        merchant.SetActive(true);
    }
}
