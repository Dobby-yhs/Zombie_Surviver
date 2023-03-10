using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public Text scoreText; // 점수 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public Text coinText; // 먹은 코인 표시용 텍스트
    public Text gunText; // 선택한 총 표시용 텍스트
    //public Text boardText; // 빌보드 점수 표시용 텍스트
    public GameObject reloadText; // 재장전 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 


    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo) {
        ammoText.text = "" + magAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateScoreText(int newScore) {
        scoreText.text = "Score : " + newScore; 
    }

    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(float min, float sec, int count) {
        waveText.text = "Wave Time : " + string.Format("{0:00}:{1:00}", min, sec) + "\nZombie Kill : " + count;
    }

    //현재 먹은 코인 갱싱
    public void UpdateCoinText(int coin)
    {
        coinText.text = "Coin : " + coin;
    }

    //선택한 총기 UI 표시
    public void UpdateGunText()
    {
        if (PlayerPrefs.GetInt("SelectedGun") == 0)
        {
            gunText.text = "Gun : pistol";
        }

        else if (PlayerPrefs.GetInt("SelectedGun") == 1)
        {
            gunText.text = "Gun : rifle";
        }

        else if (PlayerPrefs.GetInt("SelectedGun") == 2)
        {
            gunText.text = "Gun : sniper";
        }
    }

    //재장전 시 재장전 텍스트 출력
    public void SetActiveReloadUI(bool active)
    {
        reloadText.SetActive(active);
    }

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        gameoverUI.SetActive(active);
    }


    // 게임 재시작
    public void GameRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}