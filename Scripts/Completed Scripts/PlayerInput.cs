using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour {
    public string frontbackAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string rightleftAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    public string mouseRotateName = "MouseX"; 
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름

    // 값 할당은 내부에서만 가능
    public float frontback { get; private set; } // 감지된 앞뒤 입력값
    public float rightleft { get; private set; } // 감지된 좌우 입력값
    public Vector3 mouserotate { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값

    private bool inputEnabled = true;


    // 매프레임 사용자 입력을 감지
    private void FixedUpdate() {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            frontback = 0;
            rightleft = 0;
            mouserotate = new Vector3 (0,0,0);
            fire = false;
            reload = false;
            return;
        }

        if (inputEnabled)
        {
            // 앞뒤에 관한 입력 감지
            frontback = Input.GetAxis(frontbackAxisName);
            //좌우측 움직임
            rightleft = Input.GetAxis(rightleftAxisName);
            // 마우스 회전 움직임
            mouserotate = Input.mousePosition;
            // fire에 관한 입력 감지
            fire = Input.GetButton(fireButtonName);
            // reload에 관한 입력 감지
            reload = Input.GetButtonDown(reloadButtonName);
        }
    }

    public void DisableInput()
    {
        Debug.Log("DisableInput!");
        inputEnabled = false;
        frontback = 0;
        rightleft = 0;
        mouserotate = new Vector3 (0,0,0);
        fire = false;
        reload = false;
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }
}

