using UnityEngine;

public class RifleData : MonoBehaviour
{
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 7; // 공격력

    public int magCapacity = 25; // 탄창 용량

    public float timeBetFire = 0.36f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
}