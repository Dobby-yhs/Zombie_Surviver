using UnityEngine;

// 총알을 충전하는 아이템
public class AmmoPack : MonoBehaviour, IItem {
    public int ammo = 30; // 충전할 총알 수

    public void Use(GameObject target) {
        // 전달 받은 게임 오브젝트로부터 PlayerShooter 컴포넌트를 가져오기 시도
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();


        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}