using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    private MazeConstructor Generator;
    // Start is called before the first frame update
    void Start()
    {
        Generator = GetComponent<MazeConstructor>();
        Generator.GenerateNewMaze(13, 15);
    }
}
