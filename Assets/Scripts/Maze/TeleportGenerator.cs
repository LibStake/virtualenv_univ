using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Jobs;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

public class TeleportGenerator : MonoBehaviour
{
    public bool showDebug;
    public GameObject marker;
    public GameObject teleportPillarPrefab;
    public List<GameObject> quizRoomEntrances;
    public TeleportGenerator()
    {
    }

    public void CreateMazeTeleportSpot(int[,] maze, float cellWidth)
    {
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);
        float halfWidth = cellWidth / 2;
        
        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] >= 3)
                {
                    Vector3 pos = new Vector3(
                        (j - 1) * cellWidth + halfWidth,
                        0,
                        (i - 1) * cellWidth + halfWidth
                        );
                    float rot = _resolveDirection(maze, j, i);

                    if (maze[i, j] == 3)
                    {
                        // TP
                        GameObject gen = Instantiate(teleportPillarPrefab, pos, Quaternion.Euler(0, rot, 0));
                        gen.transform.GetChild(1).GetComponent<TeleportPillar>().dest = gen.transform;
                    } 
                    else if (maze[i, j] == 4)
                    {
                        // Ent
                        GameObject gen = Instantiate(marker, pos, Quaternion.Euler(0, rot, 0));
                        GameObject.Find("TeleportPillar_StartRoom").GetComponent<TeleportPillar>().dest = gen.transform;
                    }
                    else if (maze[i, j] == 5)
                    {
                        // Exit
                        
                    }
                    
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
}
