using System.Collections;
using UnityEngine;

// 총을 구현
public class GGG : MonoBehaviour {    // 총의 상태를 표현하는 데 사용할 타입을 선언
    
    public string gunName;  // 총의 이름

    public bool isPistol;
    public bool isRifle;
    public bool isSniper;

    public enum State {
        Ready, // 발사 준비됨
        Empty, // 탄알집이 빔
        Reloading // 재장전 중
    }
    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 탄알이 발사될 위치
    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과
    private LineRenderer bulletLineRenderer; // 탄알 궤적을 그리기 위한 렌더러
    private AudioSource gunAudioPlayer; // 총 소리 재생기

    public float fireDistance; // 사정거리
    public float fireRate;  // 연사속도, 높으면 높을 수록 연사 느려짐
    public float reloadTime;    // 재장전 속도
    public int damage;  // 공격력

    public int reloadBulletCount;   // 총의 재장전 개수(탄창 용량), 재장전 할 때 몇 발씩 될지
    public int ammoRemain; // 남은 전체 탄알
    public int magAmmo; // 현재 탄알집에 남아 있는 탄알 = currentBulletCount

    private float lastFireTime; // 총을 마지막으로 발사한 시점

}