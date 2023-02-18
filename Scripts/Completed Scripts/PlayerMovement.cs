using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도

    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디

    private Vector3 moveDistance;
    private Vector3 playerToMouse;
    private int floorMask;
    private float camRayLength = 100f;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Time.timeScale = 1.0f;
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {
        
        // 앞뒤 움직임 실행
        Move();
        Turning();

        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
        playerAnimator.SetFloat("Move", playerInput.frontback);
        playerAnimator.SetFloat("Move", playerInput.rightleft);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        //입력된 벡터의 값 결정
        moveDistance.Set(playerInput.rightleft, 0, playerInput.frontback);
        //벡터의 정규화 과정
        moveDistance = moveDistance.normalized * moveSpeed * Time.unscaledDeltaTime;
        // 리지드바디를 통해 게임 오브젝트 위치 변경
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    void Turning()
    {

        Ray camRay = Camera.main.ScreenPointToRay(playerInput.mouserotate);
        //메인 카메라에서 마우스로 찍은 포인트 좌표를 레이로 변환 

        RaycastHit floorHit;
        //raycast로부터 정보를 얻기위해 사용되는 구조체를 나타냅니다.

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        //Raycast 의 매개변수 : Ray , out hitInfo , 최대길이 , 레이어 마스크  
        {
            playerToMouse = floorHit.point - playerRigidbody.position;
            //클릭한 지점 - 월드공간에서의 트랜스폼 위치
            playerToMouse.y = 0f;

            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
            //바라볼 방향을 선택 playerToMouse

            playerRigidbody.MoveRotation(newRotatation);
            //Rigidbody.MoveRotation 리지드바디를 회전시킨다 newRotatation 기준으로
        }
        //Rigidbody의 보간 설정을 준수하여 Rigidbody를 회전하려면 Rigidbody.MoveRotation을 사용하십시오. 

        //Rigidbody 보간이 Rigidbody에서 활성화 된 경우 Rigidbody.MoveRotation을 호출하면 렌더링 된 모든 중간 프레임의 두 위치간에 부드러운 전환이 발생합니다.
        //각 FixedUpdate에서 강체를 계속 회전하려면이 옵션을 사용해야합니다.

        //강체를 한 회전에서 다른 회전으로 텔레포트하려는 경우 Rigidbody.rotation을 대신 설정하십시오.중간 위치는 렌더링되지 않습니다.
        }
    }