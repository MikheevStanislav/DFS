using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// This calss generate maze
/// </summary>
public class MazeGenerator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        MatrixGenerator();
        //
        MazeGeneratorFun(0);
    }

    //generate the field, where will be a maze
    private void MatrixGenerator()
    {
        Vector2 currentPosition = Vector3.zero;
        //cells creation
        for(int i =0; i < GameManager.Instance.N*GameManager.Instance.N; ++i)
        {
            
            currentPosition = new Vector2(i%GameManager.Instance.N, i/GameManager.Instance.N);
            GameObject cell = Instantiate(GameManager.Instance.cell, currentPosition, Quaternion.identity);
            cell.name = "Cell" + " " + i;
            GameManager.Instance.cells[i] = cell;
            
        }
        //adj creation
        for(int i = 0; i < GameManager.Instance.N* GameManager.Instance.N; ++i)
        {
            int row = i%GameManager.Instance.N;
            int col = i/GameManager.Instance.N;


            if(col - 1 >= 0)
                GameManager.Instance.Neigbors[i][2] = i-GameManager.Instance.N;
            if (row + 1 <  GameManager.Instance.N)
                GameManager.Instance.Neigbors[i][1] = i + 1;
            if (col + 1 < GameManager.Instance.N)
                GameManager.Instance.Neigbors[i][0] = i + GameManager.Instance.N;
            if (row - 1 >= 0)
                GameManager.Instance.Neigbors[i][3] = i - 1;
            
        }
    }
    //adj checker
    private void Debugger_for_Matrix()
    {
        for(int i = 0;i < GameManager.Instance.N*GameManager.Instance.N; ++i)
        {
            if(GameManager.Instance.Neigbors[i][0] != null)
                Debug.Log("High" + i + ":" +GameManager.Instance.Neigbors[i][0]);
            if (GameManager.Instance.Neigbors[i][1] != null)
                Debug.Log("Right" + i + ":" + GameManager.Instance.Neigbors[i][1]);
            if (GameManager.Instance.Neigbors[i][2] != null)
                Debug.Log("Low" + i + ":" + GameManager.Instance.Neigbors[i][2]);
            if (GameManager.Instance.Neigbors[i][3] != null)
                Debug.Log("Left" + i + ":" + GameManager.Instance.Neigbors[i][3]);

        }
    }
    //backtracking script for deleting the walls, and making the maze
    private void MazeGeneratorFun(int i)
    {
        GameObject cell = GameManager.Instance.cells[i];
        GameManager.Instance.flags[i] = true;
        int dir = GetRanddir(cell.name);
        while (dir != -1)
        {
            Debug.Log("randVal:" + dir);
            int j = GameManager.Instance.Neigbors[i][dir];
            Transform[] childObjects1 = cell.GetComponentsInChildren<Transform>();
            Transform walltoremove1 = childObjects1[dir+1];
            Debug.Log(walltoremove1.name);
            Destroy(walltoremove1.gameObject);

            Debug.Log(GameManager.Instance.cells[j]);

            Transform[] childObjects2= GameManager.Instance.cells[j].GetComponentsInChildren<Transform>();
            Transform walltoremove2 = childObjects2[(dir + 2)%4+1];
            Debug.Log(walltoremove2.name);
            Destroy(walltoremove2.gameObject);
            MazeGeneratorFun(j);


            dir = GetRanddir(cell.name);  
        }
    }

    private int GetRanddir(string name)
    {
        
        int j = nameToInt(name);
        GameObject cell = GameManager.Instance.cells[j];
        
        List<int> list = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            GameObject neighbor = GameManager.Instance.cells[GameManager.Instance.Neigbors[j][i]];
            if (neighbor != null && !GameManager.Instance.flags[(nameToInt(neighbor.name))])
            {
                list.Add(i);
            }
        }
        if (list.Count == 0)
        {
            return (-1);
        }
        int randomIndex = Random.Range(0, list.Count);
        int randomValue = list[randomIndex];
        return (randomValue);
    }

    private int nameToInt(string name)
    {
        string[] strings = name.Split(' ');
        return (int.Parse(strings[1]));
    }
}


