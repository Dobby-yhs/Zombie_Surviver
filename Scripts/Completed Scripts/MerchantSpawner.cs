using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MerchantSpawner : MonoBehaviour 
{
    public Merchant merchant;

    public Transform playerTransform;

    public float maxDistance = 5f;

    public bool isSpawned = false;

    public int spawnNum = 0;

    public ZombieSpawner zombie;

    
    private void Update()
    {
        if (zombie.zombieKill == 1 && !isSpawned && spawnNum == 0)
        {
            Spawn();
            merchant.Active();
            isSpawned = true;
        }
        if (merchant.merchant)
        {
            if (merchant.merchant.activeSelf == false)
            {
                isSpawned = false;
            }
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(playerTransform.position, maxDistance);

        merchant = Instantiate(merchant, spawnPosition, Quaternion.identity);

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
