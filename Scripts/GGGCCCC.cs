using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGGCCCC : MonoBehaviour
{
    public static bool isActivate = true;   // 활성화 여부

    [SerializeField]
    private GGG currentGun; // 현재 들고 있는 총, Gun.cs 할당

    private float currentFireRate; // 이 값이 0 보다 큰 동안에는 총알이 발사 되지 않는다. 초기값은 연사 속도인 Gun.cs의 fireRate 

    private bool isReload = false;  // 재장전 중인지. 


    // 연사속도
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;  // 즉, 1 초에 1 씩 감소시킨다.
    }

    // 발사 시도
    public void Fire() {
        if (state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire) {
            lastFireTime = Time.time;
            // PistolController.GetComponenet<PistolController>().Shot();
            Shot();
        }
    }    

    // 발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    private IEnumerator ShotEffect(Vector3 hitPosition) {   // 새로 추가
        muzzleFlashEffect.Play();

        shellEjectEffect.Play();

        gunAudioPlayer.PlayOneShot(gunData.shotClip);

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
    public bool Reload() {  // TryReload() -> Reload()
        if (state == State.Reloading || magAmmo >= gunData.reloadBulletCount) {
            return false;
        }

        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() {   // ReloadCoroutine() -> ReloadRoutine()
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;
      
        gunAudioPlayer.PlayOneShot(gunData.reloadClip);

        // 재장전 소요 시간 만큼 처리 쉬기
        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToFill = gunData.reloadBulletCount - magAmmo;

        if (ammoRemain < ammoToFill) {
            ammoToFill = ammoRemain;
        }

        magAmmo += ammoToFill;

        ammoRemain -= ammoToFill;
        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }

    public void GunChange(GGG _gun) {
        if (GunManager.currentWeapon != null) {
            GunManager.currentWeapon.gameObject.SetActive(false);
        }

        currentGun = _gun;
        GunManager.currentWeapon = currentGun.GetComponent<Transform>();
        
        currentGun.gameObject.SetActive(true);

        isActivate = true;
    }
}