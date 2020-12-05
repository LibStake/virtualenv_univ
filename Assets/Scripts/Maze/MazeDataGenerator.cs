using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeDataGenerator
{
    public float placementThreshold;
    private int minimumSurroundings;

    public MazeDataGenerator()
    { 
        minimumSurroundings = 5;
        placementThreshold = .1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols, int numSpots)
    {
        Debug.Log("Generate Maze.");
        // Generate 2nd Matrix 0 - Wall / 1 - Space
        int[,] maze = new int[sizeRows, sizeCols];
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);
        
        for (int i = 0; i < sizeRows; i++)
            for (int j = 0; j < sizeCols; j++)
                maze[i, j] = 1;

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 0;
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > placementThreshold)
                    {
                        maze[i, j] = 0;
                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 0;
                    }
                }
            }
        }

        int[,] markedMaze = maze.Clone() as int[,];
        try
        {
            markedMaze = ResolveSpots(maze, numSpots);
        }
        catch (InvalidDataException e)
        {
            Debug.Log("Fail to fulfill room spot min");
            markedMaze = FromDimensions(sizeRows, sizeCols, numSpots);
        }
        return markedMaze;
    }

    private int[,] ResolveSpots(int[,] maze, int numSpots)
    {
        // Mark Maze below
        // Wall       : 0
        // Open-space : 1
        // Surrounded : 2
        // Entrance   : 3
        // QuizRoom   : 4
        // Get Chamber candidate
        Debug.Log("Generate Marked Maze.");
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);
        List<int[]> surroundingIndice = new List<int[]>(); 

        for (int i = 1; i < rMax; i++)
        {
            for (int j = 1; j < cMax; j++)
            {
                if (maze[i, j] == 0) continue;
                if ((maze[i - 1, j - 1] + maze[i - 1, j] + maze[i - 1, j + 1] +
                          maze[i, j - 1] + maze[i, j + 1] +
                          maze[i + 1, j - 1] + maze[i + 1, j] + maze[i + 1, j + 1]) == 1) // Surrounding
                {
                    maze[i, j] = 2;
                    surroundingIndice.Add(new int[] {i, j});
                }
                // else : Not-Surrounding space or Wall
            }
        }

        if (surroundingIndice.Count < minimumSurroundings || surroundingIndice.Count < numSpots)
            throw new InvalidDataException("Minimum surrounding spot not fulfilled.");

        // Make maze have numSpot-1 room spot & 1 entrance spot
        for (int i = 0; i < numSpots; i++)
        {
            int[] pop = popRandom(ref surroundingIndice);
            maze[pop[0], pop[1]] = (i == 0) ? 3 : 4;
        }

        return maze;
    }

    private int[] popRandom(ref List<int[]> list)
    {
        int i = (int) (Random.value * 1000) % list.Count;
        int[] popped = list[i].ToArray();
        list.RemoveAt(i);
        return popped;
    }
}
