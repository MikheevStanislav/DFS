using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cell;                 //cell prefab
    public int N;                           //length of the square(of maze)
    public GameObject[] cells;              //cells array
    public int[][] Neigbors;                //adj
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
        Neigbors = new int[N * N][];
        for (int i = 0; i < N * N; i++)
        {
            Neigbors[i] = new int[4];
        }
    }


}
