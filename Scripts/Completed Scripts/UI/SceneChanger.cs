using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{ 
    public void SceneChange_Select()
    {
        SceneManager.LoadScene("SelectMenu");
    }

    public void SceneChange_Start()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void SceneChange_Ingame()
    {
        SceneManager.LoadScene("Ingame");
    }
}
