using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Pause : MonoBehaviour
{
    //업그레이드 UI 영역
    public static bool IsGamePause = false;

    public static GameObject CharacterSelect;
    public static GameObject pauseMenu;
    public static GameObject Start_Canvas;
    private void Start()
    {
        CharacterSelect = GameObject.Find("CharacterSelect");
        pauseMenu = FindInactiveObjectByName("pauseMenu");
    }

    

    public static void GameStartButton()
    {
        CharacterSelect.SetActive(false);
        Time.timeScale = 1f;
    }

    public static void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsGamePause = false;
    }

    public static void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsGamePause = true;
    }

    private GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        foreach (Transform obj in objs)
        {
            if (obj.hideFlags == HideFlags.None)
            {
                if (obj.name == name)
                {
                    return obj.gameObject;
                }
            }
        }
        return null;
    }

}
