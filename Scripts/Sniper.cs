﻿using System.Collections;
using UnityEngine;

// 총을 구현
public class Sniper : MonoBehaviour {
    // 총의 상태를 표현하는 데 사용할 타입을 선언
    public enum State {
        Ready, // 발사 준비됨
        Empty, // 탄알집이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 탄알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 탄알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기

    public SniperData sniperData; // 총의 현재 데이터

    private float fireDistance = 50f; // 사정거리

    public int magAmmo; // 현재 탄알집에 남아 있는 탄알

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake() {
        // 사용할 컴포넌트의 참조 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;

        bulletLineRenderer.enabled = false;
    }

    private void OnEnable() {        // 총 상태 초기화
        magAmmo = sniperData.magCapacity;
        state = State.Ready;
        lastFireTime = 0;
    }

    // 발사 시도
    public void Fire() {
        if (state == State.Ready && Time.time >= lastFireTime + sniperData.timeBetFire) {
            lastFireTime = Time.time;
            Shot();
        }
    }

    // 실제 발사 처리
    private void Shot() {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance)) {
            
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null) {
                target.OnDamage(sniperData.damage, hit.point, hit.normal);

                hitPosition = hit.point;
            }
            else {
                hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
            }

            StartCoroutine(ShotEffect(hitPosition));

            magAmmo--;
            if (magAmmo <= 0) {
                state = State.Empty;
            }
        }
    }

    // 발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    private IEnumerator ShotEffect(Vector3 hitPosition) {
        muzzleFlashEffect.Play();

        shellEjectEffect.Play();

        gunAudioPlayer.PlayOneShot(sniperData.shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);

        bulletLineRenderer.SetPosition(1, hitPosition);
        
        // 라인 렌더러를 활성화하여 탄알 궤적을 그림
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 탄알 궤적을 지움
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool Reload() {
        if (state == State.Reloading || magAmmo >= sniperData.magCapacity) {
            return false;
        }

        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;

        // 재장전 텍스트 출력
        UIManager.instance.SetActiveReloadUI(true);

        gunAudioPlayer.PlayOneShot(sniperData.reloadClip);

        // 재장전 소요 시간 만큼 처리 쉬기
        yield return new WaitForSeconds(sniperData.reloadTime);

        magAmmo = sniperData.magCapacity;
        
        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;

        //재장전 UI 비활성화
        if (state == State.Ready)
        {
            UIManager.instance.SetActiveReloadUI(false);
        }
    }
}