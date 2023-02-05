using System.Collections;
using UnityEngine;

// 총을 구현
public class GunStat : MonoBehaviour {
    private GunStat currentGun;


    private float fireDistance = 50f; // 사정거리

    public int magAmmo; // 현재 탄알집에 남아 있는 탄알

    private float lastFireTime; // 총을 마지막으로 발사한 시점


    public float damage = 30; // 공격력

    public int startAmmoRemain; // 처음에 주어질 전체 탄약
    public int magCapacity; // 탄창 용량

    public float timeBetFire; // 총알 발사 간격
    public float reloadTime; // 재장전 소요 시간
}