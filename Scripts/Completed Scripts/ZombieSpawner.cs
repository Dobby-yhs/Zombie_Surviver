using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour {
    public Zombie zombiePrefab; // 생성할 좀비 원본 프리팹

    public static bool GameIsPaused = false;

    public MerchantSpawner merchant;

    public GameObject playerObject;

    public ZombieStat zombieStat;
    public LightZombieStat lightzombieStat;
    public ZombieDogStat zombiedogStat;
    public EliteZombieStat elitezombieStat;

    public Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

    private List<Zombie> lightzombies = new List<Zombie>(); // 생성된 좀비들을 담는 리스트
    private List<Zombie> zombies = new List<Zombie>();
    private List<Zombie> zombiedogs = new List<Zombie>();
    private List<Zombie> elitezombies = new List<Zombie>();
    private float waveTime;// 현재 웨이브
    private float checkTime_1;
    private float checkTime_2;
    private float checkTime_3;
    private float checkTime_4;

    private int wave = 0;

    public int zombieKill = 0;

    private int spawnCount = 0;

    private void Start() 
    {
        waveTime = 0.0f;
    }

    private void Update() 
    {
        if (Time.timeScale == 0.0f)
        {
            Debug.Log("0.0f!");
        }
        else
        {
            waveTime += 1.0f / Time.deltaTime;

            // Debug.Log("waveTime : " + waveTime);

        // 4초에 한번씩 생성
            if (waveTime % 3.0f == 0)
            {
                CreateZombie();
            }

            // 게임 오버 상태일때는 생성하지 않음
            if (GameManager.instance != null && GameManager.instance.isGameover)
            {
                return;
            }

            
                
            if (merchant.isSpawned == true)
            {
                Debug.Log("Pause!");
                // int zombieCount = spawnCount - zombieKill;  // ??
                PauseZombies();
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                Resumezombies();
            }

            if (merchant.isSpawned == false)
            {
                Debug.Log("Resume");
                Resumezombies();
            }
            
        }

        

        // 좀비를 20마리 정도 물리친 경우 다음 스폰 실행
        // if (zombies.Count <= 5)
        // {
        //     SpawnWave();
        // }

        // 일정 시간 지난 후 좀비 스탯 증가시키는 부분
        // if (waveTime >= 10f) {
        //     lightzombieStat.damage += 5;
        //     zombieStat.damage += 5;
        //     zombiedogStat.damage += 5;
        //     elitezombieStat.damage += 5;
        // }

        
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

    private void PauseZombies() 
    {
        Time.timeScale = 0.0f;

        // player 오브젝트 unscaledDeltaTime 확인
        

        GameIsPaused = true;
    }

    private void Resumezombies() 
    {
        Time.timeScale = 1.0f;
        GameIsPaused = false;    
    }

    private void SpawnWave() {
        wave++;
        Debug.Log("SpawnWave!!");
        Debug.Log("SpawnCount : " + spawnCount);

        
        // spawnCount = 1;
        if (zombieKill == 0) {
            spawnCount = wave;
            Debug.Log(spawnCount);

        }
        
        // 좀비 수 조정하는 부분
        if (zombieKill == 15) {
            spawnCount = Mathf.RoundToInt(wave * 1.5f);     // 현재 웨이브 * 1.5를 반올림한 수만큼 좀비 생성
            Debug.Log("15!SpawnWave!!");
        }
        if (zombieKill == 150) {
            spawnCount = Mathf.RoundToInt(wave * 1.3f);
        }
        
        // spawnCount만큼 좀비 생성
        for ( int i = 0; i < spawnCount; i++)
        {
            // 좀비 생성 처리 실행
            CreateZombie();
            Debug.Log("Spawn and Create!!");
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

        // 생성한 좀비의 능력치 설정 및 생성된 좀비를 리스트에 추가
    
        zombie.ZombieSetup(zombieStat);
        zombies.Add(zombie);   

        Debug.Log("CreateZombie!!");

        // zombie.LightZombieSetup(lightzombieStat);
        // lightzombies.Add(zombie);

        if (zombieKill >= 10)
        {
            // zombie.ZombieSetup(zombieStat);
            // zombies.Add(zombie);
            Debug.Log("10!CreateZombie!!");
        }
        
        // if (zombieKill >= 90)
        // {
        //     zombie.ZombieDogSetup(zombiedogStat);
        //     zombiedogs.Add(zombie);
        // }

        // if (zombieKill >= 150)
        // {
        //     zombie.EliteZombieSetup(elitezombieStat);
        //     elitezombies.Add(zombie);
        // }
                

        // 좀비의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 좀비를 리스트에서 제거
        zombie.onDeath += () => lightzombies.Remove(zombie);
        zombie.onDeath += () => zombies.Remove(zombie);
        zombie.onDeath += () => zombiedogs.Remove(zombie);
        zombie.onDeath += () => elitezombies.Remove(zombie);

        // 사망한 좀비를 10초 뒤에 파괴
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        // 좀비 사망 시 좀비 상승
        zombie.onDeath += () => GameManager.instance.AddScore(100);

        zombie.onDeath += () => zombieKill++;
        Debug.Log("ZombieKill : " + zombieKill);
    }
}