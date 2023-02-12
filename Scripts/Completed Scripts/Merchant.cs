using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 내비메쉬 관련 코드

public class Merchant : MonoBehaviour
{
    public GameObject merchant;
    private Rigidbody merchantRigidbody;    // 필요있는지 정확히 모르겠음;;
    
    // private Animator merchantAnimator; // idle 애니메이션 추가, idle 애니메이션 고정으로 넣을거라 Start()를 통해 컴포넌트 안 받아와도 됨.
    public Transform playerTransform;   // 플레이어의 트랜스폼

    public GameObject testButton;

    public float maxDistance = 5f; // 플레이어 위치로부터 상인이 스폰될 최대 반경

    private bool isSpawned = false;

    public ZombieSpawner zombie;   // zombieKill
    public PlayerInput playerInput;


    private void Update()
    {
        if (zombie.zombieKill == 1 && !isSpawned)
        {
            Spawn();
            isSpawned = true;
            Debug.Log("merchant spawn!");
        }

        if (testButton.activeSelf == true) {
            if (Input.GetKeyDown(KeyCode.V)) {
                testButton.SetActive(false);    // 상점 UI 비활성화
                // merchant.SetActive(false);
                Destroy(merchant);
                playerInput.EnableInput();
            }
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(playerTransform.position, maxDistance);
        spawnPosition += Vector3.up;

        merchant = Instantiate(merchant, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerCharacter")
        {
            Debug.Log("Collision!");
            testButton.SetActive(true);     // 상점 UI 활성화
            playerInput.DisableInput();
        }        
    }
}
