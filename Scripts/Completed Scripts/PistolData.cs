using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/PistolData", fileName = "Pistol Data")]
public class PistolData : ScriptableObject
{
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 5; // 공격력

    public int magCapacity = 15; // 탄창 용량

    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
}