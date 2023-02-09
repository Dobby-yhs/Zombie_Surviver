using System.Collections.Generic;
using UnityEngine;

// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour {
    public Zombie zombiePrefab; // 생성할 좀비 원본 프리팹

    public Zombie zombie;

    public ZombieStat zombieStat;
    public LightZombieStat lightzombieStat;
    public ZombieDogStat zombiedogStat;
    public EliteZombieStat elitezombieStat;

    public Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

    private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비들을 담는 리스트
    private float waveTime;// 현재 웨이브
    private int wave = 0;

    private void Start() {
        waveTime = Time.time;
    }

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        // 좀비를 모두 물리친 경우 다음 스폰 실행
        if (zombies.Count <= 0)
        {
            SpawnWave();
        }

        // 일정 시간 지난 후 좀비 스탯 증가시키는 부분
        if (waveTime >= 10f) {
            lightzombieStat.damage += 5;
            zombieStat.damage += 5;
            zombiedogStat.damage += 5;
            elitezombieStat.damage += 5;
        }

        // UI 갱신
        // UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    /*
    private void UpdateUI() {
        // 현재 진행된 시간와 남은 적 수 표시
        UIManager.instance.UpdateWaveText((int)waveTime, zombies.Count);    
    }
    */
    // 현재 웨이브에 맞춰 좀비들을 생성
    private void SpawnWave() {
        wave++;
        int spawnCount = 0;
        
        // 좀비 수 조정하는 부분
        if (zombie.zombieKill == 60) {
            spawnCount = Mathf.RoundToInt(wave * 1.5f);     // 현재 웨이브 * 1.5를 반올림한 수만큼 좀비 생성
        }
        if (zombie.zombieKill == 150) {
            spawnCount = Mathf.RoundToInt(wave * 1.3f);
        }
        
        // spawnCount만큼 좀비 생성
        for ( int i = 0; i < spawnCount; i++)
        {
            // 좀비 생성 처리 실행
            CreateZombie();
        }
    }

    // 좀비를 생성하고 생성한 좀비에게 추적할 대상을 할당
    private void CreateZombie() {
        // 사용할 좀비 데이터 랜덤으로 결정
        // ZombieData zombieData = zombieDatas[Random.range(0, zombieDatas.Length)];

        // 생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 좀비 프리팹으로부터 좀비 생성
        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        // 생성한 좀비의 능력치 설정
        zombie.LightZombieSetup(lightzombieStat);
        zombie.ZombieSetup(zombieStat);
        zombie.ZombieDogSetup(zombiedogStat);
        zombie.EliteZombieSetup(elitezombieStat);

        // 생성된 좀비를 리스트에 추가
        zombies.Add(zombie);

        // 좀비의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 좀비를 리스트에서 제거
        zombie.onDeath += () => zombies.Remove(zombie);
        // 사망한 좀비를 10초 뒤에 파괴
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        // 좀비 사망 시 좀비 상승
        zombie.onDeath += () => GameManager.instance.AddScore(100);
    }
}