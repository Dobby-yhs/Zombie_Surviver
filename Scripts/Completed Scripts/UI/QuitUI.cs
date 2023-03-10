using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitUI : MonoBehaviour
{
    //����Ƽ �����Ϳ����� quit�� �������� ���� quit�� �����Ͽ���.
    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying =false;
        PlayerPrefs.DeleteAll();
#else
        Application.Quit();
        PlayerPrefs.DeleteAll();
#endif
    }
}
