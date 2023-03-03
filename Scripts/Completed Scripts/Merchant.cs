using System;   // for Action
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public GameObject merchant;

    public GameObject merchantUI;

    private PlayerInput playerInput;

    public IngameUpgrade exitbutton;

    public static event Action<bool> IsCollideChanged;

    public bool isCollide = false;

    
    private void Start()
    {
        playerInput = GameObject.FindObjectOfType<PlayerInput>();
    }

    private void Update() 
    {
        if (isCollide == true)
        {
            IsCollideChanged?.Invoke(isCollide);

            if (exitbutton.exit)
            {
                exitbutton.ExitButton();
                merchantUI.SetActive(false);
                merchant.SetActive(false);
                
                playerInput.EnableInput();

                isCollide = false;
                IsCollideChanged?.Invoke(isCollide);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide!!");
            merchantUI.SetActive(true);
            playerInput.DisableInput();
            isCollide = true;
        }
    }

    // public void Active()
    // {
    //     merchant.SetActive(true);
    // }
}