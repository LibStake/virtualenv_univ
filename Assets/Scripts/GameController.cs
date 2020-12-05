using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    
    [SerializeField] private int sizeRows;
    [SerializeField] private int sizeCols;
    [SerializeField] private float mazeCellWidth;
    [SerializeField] private float mazeCellHeight;
    private MazeConstructor MazeGenerator;
    private TeleportGenerator TeleportGenerator;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("+=+= Gen Map =+=+");
        MazeGenerator = GetComponent<MazeConstructor>();
        MazeGenerator.GenerateNewMaze(sizeRows, sizeCols, mazeCellWidth, mazeCellHeight);
        Debug.Log("+=+= Regen Map Finished =+=+");
        
        Debug.Log("+=+= Gen Teleport =+=+");
        TeleportGenerator = GetComponent<TeleportGenerator>();
        TeleportGenerator.CreateMazeTeleportSpot(MazeGenerator.Data, mazeCellWidth);
        Debug.Log("+=+= Gen Teleport Finished =+=+");
    }
}
