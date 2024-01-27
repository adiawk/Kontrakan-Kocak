using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Detection : MonoBehaviour
{
    PlayerManager player;
    NPC_AI ai;

    [SerializeField] RoomIdentifier roomIdentifier;

    private void Awake()
    {
        TryGetComponent(out ai);
        TryGetComponent(out ai);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(ai.states != NPC_State.AWARE && ai.states != NPC_State.CHASING)
        {
            if (roomIdentifier.currentRoom == player.roomIdentifier.currentRoom)
            {
                ai.SwitchState(NPC_State.AWARE);
            }
        }
        
    }
}
