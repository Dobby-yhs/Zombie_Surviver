using UnityEngine;

public class PistolData : MonoBehaviour
{
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public int damage = 5; // 공격력

    public int magCapacity = 15; // 탄창 용량

    public float timeBetFire = 0.24f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간

    private void OnEnable()
    {
        IngameUpgrade.IsChanged_D += OnDamageIsChanged;
        IngameUpgrade.IsChanged_R += OnReloadIsChanged;
        IngameUpgrade.IsChanged_M += OnMagcapacityIsChanged;
        IngameUpgrade.IsChanged_T += OnTimebetfireIsChanged;
    }

    private void OnDisable()
    {
        IngameUpgrade.IsChanged_D -= OnDamageIsChanged;
        IngameUpgrade.IsChanged_R -= OnReloadIsChanged;
        IngameUpgrade.IsChanged_M -= OnMagcapacityIsChanged;
        IngameUpgrade.IsChanged_T -= OnTimebetfireIsChanged;
    }

    private void OnDamageIsChanged(int value)
    {
        damage = value;
    }

    private void OnReloadIsChanged(float value)
    {
        reloadTime = value;
    }

    private void OnMagcapacityIsChanged(int value)
    {
        magCapacity = value;
    }

    private void OnTimebetfireIsChanged(float value)
    {
        timeBetFire = value;
    }
}
