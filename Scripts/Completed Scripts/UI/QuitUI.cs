using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitUI : MonoBehaviour
{
    //유니티 에디터에서의 quit과 빌드했을 때의 quit을 구현하였다.
    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying =false;
#else
        Application.Quit();
#endif
    }
}
