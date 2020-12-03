using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    public float placementThreshold;

    public MazeDataGenerator()
    {
        placementThreshold = .1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
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
                        // 3
                        maze[i, j] = 0;
                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 0;
                    }
                }
            }
        }

        return ResolveSpots(maze);
    }

    private int[,] ResolveSpots(int[,] maze)
    {
        // Mark Maze below
        // Open-space : 1
        // Wall       : 0
        // Surrounding-space : 2
        // Left-Entrance : 3
        // Get Chamber candidate
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 1; i < rMax; i++)
        {
            for (int j = 1; j < cMax; j++)
            {
                if (i == 1 && j == 1)
                {
                    maze[i, j] = 3; // Start Pos
                }
                else if ((maze[i - 1, j - 1] + maze[i - 1, j] + maze[i - 1, j + 1] +
                          maze[i, j - 1] + maze[i, j + 1] +
                          maze[i + 1, j - 1] + maze[i + 1, j] + maze[i + 1, j + 1]) == 1) // Surrounding
                {
                    maze[i, j] = 2;
                }
                // else : Not-Surrounding space or Wall
            }
        }

        return maze;
    }
}
