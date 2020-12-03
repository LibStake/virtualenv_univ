using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGenerator
{
    private List<Vector4> quizeRoomVectors;
    public TeleportGenerator()
    {
        
    }

    public void createMazeTeleportSpot(int[,] maze, int sizeRows, int sizeCols, float cellWidth, float cellHeight)
    {
        List<Vector4> mazeTeleportVectors = new List<Vector4>(); 
        for (int i = 1; i < sizeRows; i++)
        {
            for (int j = 1; j < sizeCols; j++)
            {
                if (maze[i, j] == 4)
                {
                    mazeTeleportVectors.Add(new Vector4(
                        cellWidth * (0.5f + i),
                        0f,
                        cellWidth * (0.5f + i),
                        _resolveDirection(maze, i, j)
                        ));
                }
            }
        }
        
        
    }

    private float _resolveDirection(int[,] maze, int x, int z)
    {
        if (maze[x - 1, z] > 0) return 0f;
        else if (maze[x, z - 1] > 0) return 90f;
        else if (maze[x + 1, z] > 0) return 180f;
        else return 270f;
    }

    private void createPillar()
    {
        // Position pillars filled with script
    }
}
