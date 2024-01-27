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
        if(ai.states != NPC_State.STUNNED && ai.states != NPC_State.AWARE && ai.states != NPC_State.CHASING)
        {
            if(player.hideAbility.isVisible && IsInAwareRangeX)
            {
                ai.SwitchState(NPC_State.AWARE);
                //if (roomIdentifier.currentRoom == player.roomIdentifier.currentRoom)
                //{
                //    ai.SwitchState(NPC_State.AWARE);
                //}
            }
        }
        
    }

    public bool IsInAwareRangeX
    {
        get
        {
            float distance = Mathf.Abs(transform.position.x - player.transform.position.x);
            return distance < 8f;
        }
    }
}
