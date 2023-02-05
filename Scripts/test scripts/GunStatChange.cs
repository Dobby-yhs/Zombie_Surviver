using System.Collections;
using UnityEngine;
using UnityEngine.UI;


// 총을 구현
public class GunStatChange : MonoBehaviour {
    // 총의 상태를 표현하는 데 사용할 타입을 선언

    // public string gunName;

    // public static bool isActivate;

    GameObject obj;

    public Button button;

    public GameObject gunObject;

    void Start() {
        button.onClick.AddListener(change);
        obj = GameObject.Find("GunStat");
    }

    void change() {
        Debug.Log("change!");
        obj.GetComponent<GunStat>().damage += 5;
        Debug.Log(obj.GetComponent<GunStat>().damage);
    }
    

    



}