using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseUI : MonoBehaviour
{
    //일시 정지를 눌렀을 때 나오는 UI창 구현 함수들이다.
    public static bool IsGamePause = false;

    public static GameObject pauseMenuUI;

    public static void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePause = false;
    }

    public static void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePause = true;
    }

   
}
