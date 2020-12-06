using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    // 게임 클리어 하는법
    // GameObject.Find("Controller").GetComponent<GameMaster>().setQuizClear(); 실행
    
    public GameObject statusText;
    public int numToClearGame = 1;
    private int gameCleared = 0;

    public void Start()
    {
        statusText.GetComponent<UnityEngine.UI.Text>().text =
            "Quiz Room " + gameCleared + " / " + numToClearGame + " cleared.";
    }

    public void setQuizClear()
    {
        gameCleared += 1;
        statusText.GetComponent<UnityEngine.UI.Text>().text =
            "Quiz Room " + gameCleared + " / " + numToClearGame + " cleared.";

        if (numToClearGame == gameCleared)
        {
            setGameClear();  
        }
        
    }

    private void setGameClear()
    {
        // Game cleared
        statusText.GetComponent<UnityEngine.UI.Text>().text = "You cleared all quiz!\nFind Yellow lit portal!";
        GameObject.Find("GameClearPortal").transform.GetChild(1).GetComponent<TeleportPillar>().SetEnable(true);
    }
}
