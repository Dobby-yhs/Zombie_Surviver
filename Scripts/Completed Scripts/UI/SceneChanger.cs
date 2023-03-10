using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{ 
    //���� ��ȯ�Ҷ� �ʿ��� �Լ����̴�. �̸� Ȱ���Ͽ� ��ư�� �����Ͽ���.
  

    public void SceneChange_Start()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void SceneChange_SelectMenu()
    {
        SceneManager.LoadScene("SelectMenu");
    }

    public void SceneChange_InGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
