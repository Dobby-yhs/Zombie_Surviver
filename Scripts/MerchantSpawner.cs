using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MerchantSpawner : MonoBehaviour 
{
    // public Merchant merchant;
    public GameObject merchantPrefab;
    private GameObject merchant;

    public Transform playerTransform;

    public float maxDistance = 20f;

    public bool isSpawned = false;

    public int spawnNum = 0;

    public ZombieSpawner zombie;
    
    
    private void Update()
    {
        if (zombie.zombieKill == 30 && !isSpawned && spawnNum == 0)
        {
            Spawn();
            merchant.SetActive(true);
            isSpawned = true;
        }
        if (zombie.zombieKill == 60 && !isSpawned && spawnNum == 1)
        {
            Spawn();
            merchant.SetActive(true);
            isSpawned = true;
        }
        if (zombie.zombieKill == 100 && !isSpawned && spawnNum == 2)
        {
            Spawn();
            merchant.SetActive(true);
            isSpawned = true;
        }
        if (zombie.zombieKill == 160 && !isSpawned && spawnNum == 3)
        {
            Spawn();
            merchant.SetActive(true);
            isSpawned = true;
        }

        if (merchant != null)
        {
            if (merchant.activeSelf == false)
            {
                isSpawned = false;
            }
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(playerTransform.position, maxDistance);

        if ( merchant != null)
        {
            Destroy(merchant);
        }

        GameObject newMerchant = Instantiate(merchantPrefab, spawnPosition, Quaternion.identity);

        merchant = newMerchant;

        spawnNum += 1;
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
