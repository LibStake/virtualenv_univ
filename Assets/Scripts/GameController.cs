using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{

    private int status = 0;
    
    [SerializeField] private int sizeRows;
    [SerializeField] private int sizeCols;
    [SerializeField] private float mazeCellWidth;
    [SerializeField] private float mazeCellHeight;
    private MazeConstructor MazeGenerator;
    private TeleportGenerator TeleportGenerator;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator = GetComponent<MazeConstructor>();
        MazeGenerator.GenerateNewMaze(sizeRows, sizeCols, mazeCellWidth, mazeCellHeight);

        TeleportGenerator = GetComponent<TeleportGenerator>();
        TeleportGenerator.CreateMazeTeleportSpot(MazeGenerator.Data, mazeCellWidth);

        
    }
}
