using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public InteractableTrap[] traps;
    public Room[] rooms;


    private void Awake()
    {
        instance = this;
    }

    public Vector3 GetAvailableReadyTrap
    {
        get
        {
            for (int i = 0; i < traps.Length; i++)
            {
                if(traps[i].isActiveAndEnabled && traps[i].isFinishBuild)
                {
                    return traps[i].transform.position;
                }
            }

            return GetRandomRoom;
        }
    }
    public Vector3 GetRandomRoom
    {
        get
        {
            return rooms[Random.Range(0, rooms.Length - 1)].transform.position;
        }
    }
}
