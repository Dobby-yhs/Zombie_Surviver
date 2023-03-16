using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using System.Xml;
using System.IO;




// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour
{
    ZombieSpawnData zombieSpawnData = new ZombieSpawnData();

    public LightZombie lightzombiePrefab;
    public Zombie zombiePrefab;
    public ZombieDog zombiedogPrefab;
    public EliteZombie elitezombiePrefab;

    public GameObject EndUI;

    public Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

    private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비들을 담는 리스트
    private List<LightZombie> lightzombies = new List<LightZombie>();
    private List<ZombieDog> zombiedogs = new List<ZombieDog>();
    private List<EliteZombie> elitezombies = new List<EliteZombie>();
    
    public LightZombieStat lightzombieStat;
    public ZombieStat zombieStat;
    public ZombieDogStat zombiedogStat;
    public EliteZombieStat elitezombieStat;


    private float waveTime = 10 * 60f;
    private float wave_min;
    private float wave_sec;

    private float checkTime = 3 * 60f;
    private float upgradeTime_lz = 0;
    private float upgradeTime_z = 0;
    private float upgradeTime_zd = 0;
    private float upgradeTime_ez = 0;

    private int wave; // 현재 웨이브

    public int zombieKill = 0;

    public float[] zombieSpawnTimes = new float[4] { 5f, 5f, 5f, 5f };
    private float[] lastSpawnTime = new float[4];

    bool merchantIsCollide;

    

    private void Start()
    {
        zombieSpawnData.Load("Assets/Resources/zombieSpawnTime.xml");

        for (int i = 0 ; i < 4; i++)
        {
            lastSpawnTime[i] = Time.time;
        }

        EndUI.SetActive(false); 
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
            waveTime -= Time.deltaTime;
            wave_min = Mathf.Floor(waveTime / 60);
            wave_sec = Mathf.RoundToInt(waveTime % 60);
            
            int i = 0;

            foreach (ZombieType zombieType in zombieSpawnData.Types)
            {
                foreach (ZombieSpawnRate spawnRate in zombieType.SpawnRates)
                {
                    if (zombieKill >= spawnRate.Min && zombieKill <= spawnRate.Max)
                    {
                        zombieSpawnTimes[i] = spawnRate.Value;
                        if (Time.time - lastSpawnTime[i] > zombieSpawnTimes[i])
                        {
                            SpawnZombie(i);

                            lastSpawnTime[i] = Time.time;
                        }
                    }
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

            if (waveTime <= 0f)
            {
                Victory();
            }

            // UI 갱신
            UpdateUI();
        }
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI()
    {
        // 현재 웨이브와 남은 적 수 표시
        UIManager.instance.UpdateWaveText(wave_min, wave_sec, zombieKill);
    }

    private void PauseZombies()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeZombies()
    {
        Time.timeScale = 1.0f;
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

    private void Victory()
    {
            PauseZombies();
            EndUI.SetActive(true);
    }

    private void CreateLightZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        LightZombie lightzombie = Instantiate(lightzombiePrefab, spawnPoint.position, spawnPoint.rotation);

        lightzombie.ZombieSetup(lightzombieStat);

        if (upgradeTime_lz >= checkTime)
        {
            lightzombieStat.health += 20f;
            lightzombieStat.damage += 10f;
            lightzombieStat.speed -= 0.5f;
            lightzombieStat.timeBetAttack -= 0.1f;

            upgradeTime_lz = 0f;
        }

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

        if (upgradeTime_z >= checkTime)
        {
            zombieStat.health += 20f;
            zombieStat.damage += 10f;
            zombieStat.speed -= 0.5f;
            zombieStat.timeBetAttack -= 0.1f;

            upgradeTime_z = 0f;
        }

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

        if (upgradeTime_zd >= checkTime)
        {
            zombiedogStat.health += 20f;
            zombiedogStat.damage += 10f;
            zombiedogStat.speed -= 0.5f;
            zombiedogStat.timeBetAttack -= 0.1f;

            upgradeTime_zd = 0f;
        }

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

        if (upgradeTime_ez >= checkTime)
        {
            elitezombieStat.health += 20f;
            elitezombieStat.damage += 10f;
            elitezombieStat.speed -= 0.5f;
            elitezombieStat.timeBetAttack -= 0.1f;

            upgradeTime_ez = 0f;
        }

        elitezombies.Add(elitezombie);

        elitezombie.onDeath += () => elitezombies.Remove(elitezombie);

        elitezombie.onDeath += () => Destroy(elitezombie.gameObject, 10f);

        elitezombie.onDeath += () => GameManager.instance.AddScore(200);

        elitezombie.onDeath += () => zombieKill++;
    }
}