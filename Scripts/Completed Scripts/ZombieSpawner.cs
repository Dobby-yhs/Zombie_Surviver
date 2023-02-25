using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour {
    // public List<GameObject> zombiePrefabs; // 생성할 좀비 원본 프리팹 배열

    public Zombie zombiePrefab;

    public LightZombie lightzombiePrefab;
    public ZombieDog zombiedogPrefab;
    public EliteZombie elitezombiePrefab;

    public Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

    private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비들을 담는 리스트
    private List<LightZombie> lightzombies = new List<LightZombie>();
    private List<ZombieDog> zombiedogs = new List<ZombieDog>();
    private List<EliteZombie> elitezombies = new List<EliteZombie>();


    public ZombieStat zombieStat;
    public LightZombieStat lightzombieStat;
    public ZombieDogStat zombiedogStat;
    public EliteZombieStat elitezombieStat;


    private float waveTime;
    private int wave; // 현재 웨이브

    public int zombieKill = 0;

    bool merchantIsCollide;


    private void Start() 
    {
        waveTime = Time.deltaTime;

        InvokeRepeating("CreateZombie", 0f, 10f);
        InvokeRepeating("CreateLightZombie", 0f, 3f);
    }

    private void OnEnable() 
    {
        Merchant.IsCollideChanged += OnMerchantIsCollideChanged;
    }

    private void OnDisable() 
    {
        Merchant.IsCollideChanged -= OnMerchantIsCollideChanged;
    }

    private void OnMerchantIsCollideChanged(bool value)
    {
        merchantIsCollide = value;
    }

    private void Update() 
    {
        if (Time.timeScale == 0.0f)
        {
            if (merchantIsCollide == false)
            {
                Debug.Log("ResumeZombie!!!!!!!!!!!!!!!!!");
                ResumeZombies();
            }
        }

        else
        {
            Debug.Log(merchantIsCollide);
            if (merchantIsCollide == true)
            {
                PauseZombies();
                Debug.Log(Time.timeScale);
                Debug.Log(merchantIsCollide);
            }

            if (GameManager.instance != null && GameManager.instance.isGameover)
            {
                return;
            } 

            // UI 갱신
            UpdateUI();
        }        
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() {
        // 현재 웨이브와 남은 적 수 표시
        UIManager.instance.UpdateWaveText(wave, zombies.Count);
    }

    private void PauseZombies()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeZombies()
    {
        Time.timeScale = 1.0f;
        

        Debug.Log(Time.timeScale);
        Debug.Log(merchantIsCollide);
    }

    // 좀비를 생성하고 생성한 좀비에게 추적할 대상을 할당
    private void CreateZombie() 
    {
        // 생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 좀비 프리팹으로부터 좀비 생성
        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        zombie.ZombieSetup(zombieStat);
        zombies.Add(zombie);

        // 좀비의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 좀비를 리스트에서 제거
        zombie.onDeath += () => zombies.Remove(zombie);
        // 사망한 좀비를 10초 뒤에 파괴
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        // 좀비 사망 시 점수 상승
        zombie.onDeath += () => GameManager.instance.AddScore(100);

        zombie.onDeath += () => zombieKill++;
    }

    private void CreateLightZombie() 
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        LightZombie lightzombie = Instantiate(lightzombiePrefab, spawnPoint.position, spawnPoint.rotation);

        lightzombie.ZombieSetup(lightzombieStat);
        lightzombies.Add(lightzombie);

        lightzombie.onDeath += () => lightzombies.Remove(lightzombie);
        
        lightzombie.onDeath += () => Destroy(lightzombie.gameObject, 10f);
        
        lightzombie.onDeath += () => GameManager.instance.AddScore(100);

        lightzombie.onDeath += () => zombieKill++;
    }

    private void CreateZombieDog() 
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        ZombieDog zombiedog = Instantiate(zombiedogPrefab, spawnPoint.position, spawnPoint.rotation);

        zombiedog.ZombieSetup(zombiedogStat);
        zombiedogs.Add(zombiedog);

        zombiedog.onDeath += () => zombiedogs.Remove(zombiedog);
        
        zombiedog.onDeath += () => Destroy(zombiedog.gameObject, 10f);
        
        zombiedog.onDeath += () => GameManager.instance.AddScore(100);

        zombiedog.onDeath += () => zombieKill++;
    }

    private void CreateEliteZombie() 
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        EliteZombie elitezombie = Instantiate(elitezombiePrefab, spawnPoint.position, spawnPoint.rotation);

        elitezombie.ZombieSetup(elitezombieStat);
        elitezombies.Add(elitezombie);

        elitezombie.onDeath += () => elitezombies.Remove(elitezombie);
        
        elitezombie.onDeath += () => Destroy(elitezombie.gameObject, 10f);
        
        elitezombie.onDeath += () => GameManager.instance.AddScore(100);

        elitezombie.onDeath += () => zombieKill++;
    }
}