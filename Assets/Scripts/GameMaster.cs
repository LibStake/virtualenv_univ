using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // 게임 클리어 하는법
    // GameObject.Find("Controller").GetComponent<GameMaster>().setQuizClear(); 실행

    public int numToClearGame = 1;
    private int gameCleared = 0;

    public void setQuizClear()
    {
        gameCleared += 1;

        if (numToClearGame == gameCleared)
        {
            setGameClear();  
        }
        
    }

    private void setGameClear()
    {
        // Game cleared
    }
}
