using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class Game_Manager : MonoBehaviour
{
    public GameEvent StartGame;
    public GameObject RestartGameObj;

    private void Awake()
    {
        RestartGameObj.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableGameOver()
    {
        RestartGameObj.SetActive(true);
    }

    public void EnableGameStart()
    {
        RestartGameObj.SetActive(false);
    }
}
