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
    public GameObject mazeEnterMarker;
    public GameObject mazeExitMarker;
    public GameObject teleportPillarPrefab;
    public List<GameObject> quizRoomEntrances;
    public TeleportGenerator()
    {
    }

    public void CreateMazeTeleportSpot(int[,] maze, float cellWidth)
    {
        Debug.Log("Get Quiz Room " + quizRoomEntrances.Count);
        
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);
        float halfWidth = cellWidth / 2;
        int idx = 0;
        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] >= 3)
                {
                    Vector3 pos = new Vector3(
                        j * cellWidth,
                        1,
                        i * cellWidth
                        );
                    float rot = _resolveDirection(maze, i, j);

                    if (maze[i, j] == 3)
                    {
                        Debug.Log("-- Entrance TP --");
                        // Ent
                        GameObject gen = Instantiate(mazeEnterMarker, pos, Quaternion.Euler(0, rot, 0));
                        GameObject.Find("TeleportPillar_StartRoom").transform.GetChild(1).GetComponent<TeleportPillar>().dest = gen.transform;
                        Debug.Log("Entrance Created to " + pos.x + "," + pos.y + "," + pos.z);
                        Debug.Log("-- Entrance TP Finished --");
                    }
                    else if (maze[i, j] == 4)
                    {
                        Debug.Log("-- Quiz Room TP --");
                        // TP
                        GameObject gen = Instantiate(teleportPillarPrefab, pos, Quaternion.Euler(0, rot, 0));
                        // Set Dest from maze
                        Debug.Log("Set Dest from Maze");
                        gen.transform.GetChild(1).GetComponent<TeleportPillar>().dest =
                            quizRoomEntrances[idx].transform.GetChild(0).transform;
                        // Set Dest from room
                        Debug.Log("Set Dest from Room");
                        quizRoomEntrances[idx].transform.GetChild(1).transform.GetComponent<TeleportPillar>().dest =
                            gen.transform.GetChild(0).transform;
                        
                        Debug.Log("Entrance Created ON-> " + pos.x + "," + pos.y + "," + pos.z + " TO-> " + 
                                  quizRoomEntrances[idx].transform.GetChild(0).transform.position.x + "," + 
                                  quizRoomEntrances[idx].transform.GetChild(0).transform.position.y + "," + 
                                  quizRoomEntrances[idx].transform.GetChild(0).transform.position.z);
                        
                        idx += 1;
                        Debug.Log("-- Quiz Room TP Finished --");
                    }
                    else if (maze[i, j] == 5)
                    {
                        // Exit
                        GameObject gen = Instantiate(teleportPillarPrefab, pos, Quaternion.Euler(0, rot, 0));
                        gen.name = "GameClearPortal";
                        gen.transform.GetChild(1).GetComponent<TeleportPillar>().dest = mazeExitMarker.transform;
                        gen.transform.GetChild(1).GetComponent<TeleportPillar>().disabled = true;
                        gen.transform.GetChild(3).GetComponent<Light>().color = Color.yellow;
                        gen.transform.GetChild(3).GetComponent<Light>().intensity = 10;
                    }
                    
                }

            }
        }
    }

    private float _resolveDirection(int[,] maze, int x, int z)
    {
        if (maze[x - 1, z] > 0) return 90f;
        else if (maze[x, z - 1] > 0) return 180f;
        else if (maze[x + 1, z] > 0) return 270f;
        else return 0f;
    }
}
