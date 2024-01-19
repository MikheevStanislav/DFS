using System;
using System.Collections;
using System.Threading;
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

    }
    //adj checker
    /*private void Debugger_for_Matrix()
    {
        for(int i = 0;i < GameManager.Instance.N*GameManager.Instance.N; ++i)
        {
            if(GameManager.Instance.neighbors[i][0] != null)
                Debug.Log("High" + i + ":" +GameManager.Instance.neighbors[i][0]);
            if (GameManager.Instance.neighbors[i][1] != null)
                Debug.Log("Right" + i + ":" + GameManager.Instance.neighbors[i][1]);
            if (GameManager.Instance.neighbors[i][2] != null)
                Debug.Log("Low" + i + ":" + GameManager.Instance.neighbors[i][2]);
            if (GameManager.Instance.neighbors[i][3] != null)
                Debug.Log("Left" + i + ":" + GameManager.Instance.neighbors[i][3]);

        }
    }*/
    //backtracking script for deleting the walls, and making the maze
    private void MazeGeneratorFun(int i)
    {
        GameObject cell = GameManager.Instance.cells[i];
        GameManager.Instance.flags[i] = true;
        int dir = GetRanddir(i);
        int[] neighbors = Neigbors(i);
        while (dir != -1)
        {
            //Debug.Log("randVal:" + dir);

            int j = neighbors[dir];
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


            dir = GetRanddir(i);  
        }
    }

    private void MazeGeneratorFUn(int start)
    {
        System.Collections.Generic.Stack<int> stack = new Stack<int>();
        stack.Push(start);
        GameManager.Instance.flags[start] = true;
        while(stack.Count > 0)
        {
            
            int s = stack.Pop();
            while(GameManager.Instance)


            if (!GameManager.Instance.flags[s])
                GameManager.Instance.flags[s] = true;
            int[] neigbors = Neigbors(s);
            foreach(int x in neigbors)
                if (!GameManager.Instance.flags[x])
                    stack.Push(x);

        }
    }

    private int GetRanddir(int j)
    {
        
        GameObject cell = GameManager.Instance.cells[j];
        int[] neighbors = Neigbors(j);
        List<int> list = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (neighbors[i] != -1 && !GameManager.Instance.flags[neighbors[i]])
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

    public int[] Neigbors(int index)
    {
        int[] neigbors = new int[4];
        for (int i = 0; i < neigbors.Length; ++i)
            neigbors[i] = -1;

        int row = index % GameManager.Instance.N;
        int col = index / GameManager.Instance.N;
        if (col + 1 < GameManager.Instance.N)
            neigbors[0] = index + GameManager.Instance.N;
        if (row + 1 < GameManager.Instance.N)
            neigbors[1] = index + 1;
        if (col - 1 >= 0)
            neigbors[2] = index - GameManager.Instance.N;
        if (row - 1 >= 0)
            neigbors[3] = index - 1;

        return (neigbors);
    }
}


