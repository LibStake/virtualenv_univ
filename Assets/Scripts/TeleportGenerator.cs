using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

public class TeleportGenerator : MonoBehaviour
{
    public bool showDebug;
    public GameObject teleportPillarPrefab;
    private List<Vector4> quizeRoomVectors;
    public TeleportGenerator()
    {
    }

    private void Awake()
    {
        // Init prop
    }

    public void CreateMazeTeleportSpot(int[,] maze, int sizeRows, int sizeCols, float cellWidth, float cellHeight)
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

        
        for (int i = 0; i < mazeTeleportVectors.Count; i++)
        {
            // Gen teleporter for maze
            Vector3 posMaze = new Vector3(mazeTeleportVectors[i].x, mazeTeleportVectors[i].y, mazeTeleportVectors[i].z);
            Quaternion rotMaze = Quaternion.Euler(0, mazeTeleportVectors[i].w, 0);
            GameObject mazePillar = Instantiate(teleportPillarPrefab, posMaze, rotMaze);

            // Gen teleporter for room
            Vector3 posRoom = new Vector3(quizeRoomVectors[i].x, quizeRoomVectors[i].y, quizeRoomVectors[i].z);
            Quaternion rotRoom = Quaternion.Euler(0, quizeRoomVectors[i].w, 0);
            GameObject roomPillar = Instantiate(teleportPillarPrefab, posRoom, rotRoom);
            
            mazePillar.GetComponent<TeleportPillar>().setDeparture(mazePillar.transform.GetChild(0).transform.position);
            mazePillar.GetComponent<TeleportPillar>().setDestination(roomPillar.transform.GetChild(0).transform.position);
            roomPillar.GetComponent<TeleportPillar>().setDeparture(roomPillar.transform.GetChild(0).transform.position);
            roomPillar.GetComponent<TeleportPillar>().setDestination(mazePillar.transform.GetChild(0).transform.position);
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
