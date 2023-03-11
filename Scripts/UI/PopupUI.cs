using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    //공부할 겸 해본 credit 버튼을 눌렀을 때 창 오픈 애니메이션
    private Animator animator;

   private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
