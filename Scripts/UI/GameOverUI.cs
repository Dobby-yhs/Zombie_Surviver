using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    //���ӿ��� �� ������ UI���� ��ư�� ������ �Լ��̴�. ����� k�� ��������
    //�۵��ϰ� �����Ͽ���. �Ŀ� ������Ʈ �� ����

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
