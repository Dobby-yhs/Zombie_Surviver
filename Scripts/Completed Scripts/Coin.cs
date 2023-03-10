using UnityEngine;

// 게임 점수를 증가시키는 아이템
public class Coin : MonoBehaviour, IItem {
    private int coin = 1;
    private int SavedCoin;
    private int InGameCoin;

    void Start()
    {
        SavedCoin = PlayerPrefs.GetInt("SavedCoin", 0);
        InGameCoin = PlayerPrefs.GetInt("InGameCoin", 0);
    }

    public void Use(GameObject target) {
        
        PlayerPrefs.SetInt("InGameCoin", coin);
        SavedCoin += coin;
        PlayerPrefs.SetInt("SavedCoin", SavedCoin + InGameCoin);
        GameManager.instance.AddCoin(coin);
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}