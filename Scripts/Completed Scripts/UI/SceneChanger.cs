using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{ 
    //씬을 전환할때 필요한 함수들이다. 이를 활용하여 버튼에 적용하였다.
  

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
