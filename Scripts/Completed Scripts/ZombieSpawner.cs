using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;




// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour
{
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

    public float[] zombieSpawnTimes = new float[4] { 5f, 5f, 5f, 5f };
    private float[] lastSpawnTime = new float[4];


    bool merchantIsCollide;


    private void Start()
    {
        waveTime = Time.deltaTime;

        for (int i = 0 ; i < 4; i++)
        {
            lastSpawnTime[i] = Time.time;
        }
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
                ResumeZombies();
            }
        }

        else
        {
            if (zombieKill <  5) {
                zombieSpawnTimes[0] = 5f;
                zombieSpawnTimes[1] = 30f;
                zombieSpawnTimes[2] = 100f;
                zombieSpawnTimes[3] = 100f;
            }
            if (zombieKill >= 5 && zombieKill < 10) {
                zombieSpawnTimes[0] = 5f;
                zombieSpawnTimes[1] = 30f;
                zombieSpawnTimes[2] = 100f;
                zombieSpawnTimes[3] = 100f;
            }
            if (zombieKill >= 20 && zombieKill < 30) {
                zombieSpawnTimes[0] = 5f;
                zombieSpawnTimes[1] = 30f;
                zombieSpawnTimes[2] = 100f;
                zombieSpawnTimes[3] = 100f;           
            }
            if (zombieKill >= 30 && zombieKill < 40) {
                zombieSpawnTimes[0] = 5f;
                zombieSpawnTimes[1] = 30f;
                zombieSpawnTimes[2] = 100f;
                zombieSpawnTimes[3] = 100f;
            }

            for (int i = 0; i < 4; i++)
            {
                if (Time.time - lastSpawnTime[i] > zombieSpawnTimes[i])
                {
                    Debug.Log("SpawnZombie");
                    Debug.Log((Time.time - lastSpawnTime[0]) + ", " + (Time.time - lastSpawnTime[1]) + ", " + (Time.time - lastSpawnTime[2]) + ", " + (Time.time - lastSpawnTime[3]));
                    Debug.Log(zombieSpawnTimes[0] + ", " + zombieSpawnTimes[1] + ", " + zombieSpawnTimes[2] + ", " + zombieSpawnTimes[3]);
                    SpawnZombie(i);

                    lastSpawnTime[i] = Time.time;
                }
            }

            if (merchantIsCollide == true)
            {
                PauseZombies();
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
    private void UpdateUI()
    {
        // 현재 웨이브와 남은 적 수 표시
        UIManager.instance.UpdateWaveText(waveTime, zombieKill);
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

    private void SpawnZombie(int index)
    {
        switch(index)
        {
            case 0: 
                CreateLightZombie();
                break;
            case 1:
                CreateZombie();
                break;
            case 2:
                CreateZombieDog();
                break;
            case 3 :
                CreateEliteZombie();
                break;
        }       
    }



    private void CreateLightZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        LightZombie lightzombie = Instantiate(lightzombiePrefab, spawnPoint.position, spawnPoint.rotation);

        lightzombie.ZombieSetup(lightzombieStat);
        lightzombies.Add(lightzombie);

        lightzombie.onDeath += () => lightzombies.Remove(lightzombie);

        lightzombie.onDeath += () => Destroy(lightzombie.gameObject, 10f);

        lightzombie.onDeath += () => GameManager.instance.AddScore(50);

        lightzombie.onDeath += () => zombieKill++;
    }

    private void CreateZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        zombie.ZombieSetup(zombieStat);
        zombies.Add(zombie);

        zombie.onDeath += () => zombies.Remove(zombie);
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        zombie.onDeath += () => GameManager.instance.AddScore(100);
        zombie.onDeath += () => zombieKill++;
    }

    private void CreateZombieDog()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        ZombieDog zombiedog = Instantiate(zombiedogPrefab, spawnPoint.position, spawnPoint.rotation);

        zombiedog.ZombieSetup(zombiedogStat);
        zombiedogs.Add(zombiedog);

        zombiedog.onDeath += () => zombiedogs.Remove(zombiedog);

        zombiedog.onDeath += () => Destroy(zombiedog.gameObject, 10f);

        zombiedog.onDeath += () => GameManager.instance.AddScore(150);

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

        elitezombie.onDeath += () => GameManager.instance.AddScore(200);

        elitezombie.onDeath += () => zombieKill++;
    }
}