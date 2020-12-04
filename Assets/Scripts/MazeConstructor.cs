using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    private MazeDataGenerator _dataGenerator;
    private MazeMeshGenerator _meshGenerator;

    [SerializeField] private int numQuizRoom;
    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    public int[,] Data
    {
        get;
        private set;
    }

    private void Awake()
    {
        Data = new int[,]
        {
            {0, 0, 0},
            {0, 1, 0},
            {0, 0, 0}
        };
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols, float mazeCellWidth, float mazeCellHeight)
    {
        _meshGenerator = new MazeMeshGenerator(mazeCellWidth, mazeCellHeight);
        _dataGenerator = new MazeDataGenerator();
        
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for maze generation.");
        }

        Data = _dataGenerator.FromDimensions(sizeRows, sizeCols, numQuizRoom + 1);
        DisplayMaze();
    }

    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = _meshGenerator.FromData(Data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] {mazeMat1, mazeMat2};
    }
    
    private void OnGUI()
    {
        if (!showDebug) return;

        int[,] maze = Data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "==";
                }
                else if (maze[i, j] == 2)
                {
                    msg += "><";
                }
                else if (maze[i, j] == 3)
                {
                    msg += "++";
                }
                else if (maze[i, j] == 4)
                {
                    msg += "[  ]";
                }
                else
                {
                    msg += "....";
                }
            }
            msg += "\n";
        }

        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
}
