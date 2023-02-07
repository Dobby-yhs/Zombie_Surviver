using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : GGGCCCC
{
    // 활성화 여부
    public static bool isActivate = true;

    void Awake() {  // private 제거
        // 사용할 컴포넌트의 참조 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();

        GunManager.currentWeapon = currentGun.GetComponent<Transform>();

        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;

        bulletLineRenderer.enabled = false;
    }

    private void OnEnable() {        // 총 상태 초기화
        // ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.reloadBulletCount;  // magCapacity -> reloadBulletCount
        state = State.Ready;
        lastFireTime = 0;
    } 

    void Update()
    {
        if (isActivate)
            GunFireRateCalc();
            Fire();
            Reload();
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    // 실제 발사 처리
    private void Shot() {
        Raycast hit;
        Vector3 hitPosition = Vector3.zero;
        currentFireRate = currentGun.fireRate;  // 연사 속도 재계산, 새로 추가

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance)) {
            
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null) {
                target.OnDamage(gunData.damage, hit.point, hit.normal);

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

    public override void GunChange(GGG _gun)
    {
        base.GunChange(_gun);
        isActivate = true;
    }
}
