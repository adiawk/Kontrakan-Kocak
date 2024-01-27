using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ROOM_NAME
{
    ROOM_1, ROOM_2, ROOM_3, ROOM_4, ROOM_5
}

public class Room : MonoBehaviour
{
    public ROOM_NAME roomName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomIdentifier room;
        collision.transform.TryGetComponent(out room);

        if (room != null)
        {
            room.currentRoom = roomName;

            //Debug.Log($"{collision.gameObject.name} In: {roomName}");
        }
    }
}
