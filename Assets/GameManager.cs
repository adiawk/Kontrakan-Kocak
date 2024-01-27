using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isEndLevel;
    public UnityEvent OnLose;
    public UnityEvent OnNextLevel;
    public UnityEvent OnTamat;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        OnLose?.Invoke();
    }

    public void CheckGameCondition()
    {
        if(RoomManager.instance.trapHitDone >= RoomManager.instance.traps.Length)
        {
            if (isEndLevel)
            {
                Tamat();
            }
            else
            {
                NextLevel();
            }
        }
    }

    public void NextLevel()
    {
        OnNextLevel?.Invoke();
    }

    public void Tamat()
    {
        OnTamat?.Invoke();
    }
}
