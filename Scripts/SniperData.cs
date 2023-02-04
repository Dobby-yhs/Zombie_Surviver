using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SniperData", fileName = "Sniper Data")]
public class SniperData : ScriptableObject
{
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 15; // 공격력

    public int magCapacity = 10; // 탄창 용량

    public float timeBetFire = 0.096f; // 총알 발사 간격
    public float reloadTime = 2.7f; // 재장전 소요 시간
}