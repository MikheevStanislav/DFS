using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cell;                 //cell prefab
    public int N;                           //length of the square(of maze)
    public GameObject[] cells;              //cells array
    public bool[] flags;                    //array of the visited cells


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        cells = new GameObject[N * N];
        flags = new bool[N * N];
    }


}
