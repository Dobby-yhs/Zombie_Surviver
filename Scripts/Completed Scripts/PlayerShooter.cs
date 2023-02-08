using UnityEngine;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter : MonoBehaviour 
{
    public GameObject[] guns;
    public Gun gun;
    public Pistol pistol;
    public Rifle rifle;
    public Sniper sniper;

    // public Transform gunPivot; // 총 배치의 기준점
    public Transform Gun;
    public Transform leftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform rightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    private PlayerInput playerInput; // 플레이어의 입력
    private Animator playerAnimator; // 애니메이터 컴포넌트

    private void Start() {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();

        foreach(GameObject gun in guns)
        {
            gun.SetActive(false);
        }

        guns[gun.currentGunIndex].SetActive(true);
    }

    private void OnEnable() {
        // 슈터가 활성화될 때 총도 함께 활성화
        guns[gun.currentGunIndex].SetActive(true);
    }
    
    /*
    private void OnDisable() {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        // currentGun.gameObject.SetActive(false);
        guns[gun.currentGunIndex].SetActive(false);

    }
    */

    private void Update() {
        // 입력을 감지하고 총 발사하거나 재장전
        
        if (playerInput.fire) {
            switch (gun.currentGunIndex)
            {
                case 0 :
                    pistol.Fire();
                    break;
                case 1 :
                    rifle.Fire();
                    break;
                case 2 :
                    sniper.Fire();
                    break;
            }
        }
        else if (playerInput.reload) {
            switch (gun.currentGunIndex)
            {  
                case 0 :
                    if (pistol.Reload()) 
                    {
                        playerAnimator.SetTrigger("Reload");
                    }
                    break;
                case 1 :
                    if (rifle.Reload()) 
                    {
                        playerAnimator.SetTrigger("Reload");
                    }
                    break;
                case 2 :
                    if (sniper.Reload()) 
                    {
                        playerAnimator.SetTrigger("Reload");
                    }
                    break;
            }
        }

        UpdateUI();
    }

    // 탄약 UI 갱신
    private void UpdateUI() {
        // if (currentGun != null && UIManager.instance != null)
        if (UIManager.instance != null)
        {
            // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
            switch (gun.currentGunIndex)
            {  
                case 0 :
                    UIManager.instance.UpdateAmmoText(pistol.magAmmo);
                    break;
                case 1 :
                    UIManager.instance.UpdateAmmoText(rifle.magAmmo);
                    break;
                case 2 :
                    UIManager.instance.UpdateAmmoText(sniper.magAmmo);
                    break;
            }
            // UIManager.instance.UpdateAmmoText(currentGun.magAmmo);
        }
    }

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex) {
        Gun.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}