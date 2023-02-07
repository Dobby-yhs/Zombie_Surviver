using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArray : MonoBehaviour
{
    public GameObject[] cubes;

    public int[] array;

    public int currentCubeIndex = 0;

    void Start()
    {
        for ( int i = 0; i < cubes.Length; i++) {
            if ( i == currentCubeIndex)
            {
                cubes[i].gameObject.SetActive(true);
            }
            else{
                cubes[i].gameObject.SetActive(false);
            }
        }
        
    }

    public void SwitchCube(int newCubeIndex)
    {
        Debug.Log("Switch!");
        cubes[currentCubeIndex].gameObject.SetActive(false);
        currentCubeIndex = newCubeIndex;
        cubes[currentCubeIndex].gameObject.SetActive(true);
    }
}
