using System.Collections;
using UnityEngine;
using UnityEngine.UI;


// 총을 구현
public class GunChange : MonoBehaviour {
    // 총의 상태를 표현하는 데 사용할 타입을 선언

    // public string gunName;

    // public static bool isActivate;

    GameObject obj;
    

    public Button button;

    public GameObject gunObject;

    public TestArray testArray;
    

    void Start() {
        button.onClick.AddListener(change);
        obj = GameObject.Find("Gun");
    }

    void change() {
        Debug.Log("change!");
        obj.GetComponent<Gun>().damage += 5;
        Debug.Log(obj.GetComponent<Gun>().damage);
        testArray.currentCubeIndex = 2;
        testArray.SwitchCube(testArray.currentCubeIndex);
        Debug.Log(testArray.currentCubeIndex);
    }
    

    



}