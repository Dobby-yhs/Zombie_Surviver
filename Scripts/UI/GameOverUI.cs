using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    //게임오버 시 나오는 UI에서 버튼에 적용할 함수이다. 현재는 k를 눌렀을때
    //작동하게 구성하였다. 후에 업데이트 할 예정

    public static bool IsGamePause = false;

    public GameObject pauseMenuUI;

    void Die()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePause = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }
}
