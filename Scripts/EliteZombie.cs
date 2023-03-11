using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드 가져오기

// 좀비 AI 구현
public class EliteZombie : LivingEntity
{
    public LayerMask whatIsTarget; // 추적 대상 레이어

    private LivingEntity targetEntity; // 추적 대상
    private NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트

    public ParticleSystem hitEffect; // 피격 시 재생할 파티클 효과
    public AudioClip deathSound; // 사망 시 재생할 소리
    public AudioClip hitSound; // 피격 시 재생할 소리

    private Animator lightzombieAnimator; // 애니메이터 컴포넌트
    // 수정필요!!!

    private AudioSource zombieAudioPlayer; // 오디오 소스 컴포넌트
    private Renderer zombieRenderer; // 렌더러 컴포넌트


    public EliteZombieStat elitezombieStat;

    public float damage; // 공격력
    public float timeBetAttack; // 공격 간격 여기서 
    private float lastAttackTime; // 마지막 공격 시점

    // 추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }

    private void Awake() {
        // 게임오브젝트로부터 사용할 컴포넌트
        navMeshAgent = GetComponent<NavMeshAgent>();

        lightzombieAnimator = GetComponent<Animator>();
        // 수정 필요!!!!

        zombieAudioPlayer = GetComponent<AudioSource>();
        zombieRenderer = GetComponentInChildren<Renderer>();
    }

    // 좀비 AI의 초기 스펙을 결정하는 셋업 메서드
    public void ZombieSetup(EliteZombieStat elitezombieStat) 
    {
        startingHealth = elitezombieStat.health;
        damage = elitezombieStat.damage;
        navMeshAgent.speed = elitezombieStat.speed;
        timeBetAttack = elitezombieStat.timeBetAttack;
        // zombieRenderer.material.color = zombieStat.skinColor;
    }

    private void Start() {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    private void Update() {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        lightzombieAnimator.SetBool("HasTarget", hasTarget);
        // 수정필요!!!!

    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로 갱신
    private IEnumerator UpdatePath()
    {
        // 살아 있는 동안 무한 루프
        while (!dead)
        {
            if (hasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                navMeshAgent.isStopped = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 40f, whatIsTarget);

                //모든 콜라이더를 순회하며 살아있는 LivingEntity 찾기
                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    if (livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;

                        break;
                    }
                }
            }
            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }

    // 데미지 처리하는 메소드
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //아직 사망하지 않은 경우에만 피격효과 재생
        if (!dead)
        {
            //공격받은 지점과 방향으로 파틱클 효과 재생
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            zombieAudioPlayer.PlayOneShot(hitSound);
        }
        // LivingEntity의 OnDamage()를 실행하여 데미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();

        //다른 ai를 방해하지 않도록 자신의 모든 콜라이더를 비활성화
        Collider[] zombieColliders = GetComponents<Collider>();
        for (int i = 0; i < zombieColliders.Length; i++)
        {
            zombieColliders[i].enabled = false;
        }

        //ai 추적을 중지하고 내비메시 컴포넌트 비활성화
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        // 다른 종류 좀비 사망 애니메이션 확인해서 넣기
        lightzombieAnimator.SetTrigger("Die");
        // 수정필요!!!!


        //사망 효과음 재생
        zombieAudioPlayer.PlayOneShot(deathSound);
    }

    private void OnTriggerStay(Collider other)
    {
        // 자신이 사망하지 않았으며
        //최근 공격 시점에서 timeBetAttack 이상 시간이 지났다면 공격가능
        if (!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            //상대방의 LivingEntity타입 가져오기 시도
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            //상대방의 LivingEntity가 자신의 추적 대상이라면 공격실행
            if (attackTarget != null && attackTarget == targetEntity)
            {
                //최근 공격시간 갱신
                lastAttackTime = Time.time;

                //상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                //공격 실행
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}