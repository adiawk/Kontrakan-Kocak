using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPC_State
{
    IDLE,
    GO_TO,
    AWARE,
    CHASING
}

public class NPC_AI : MonoBehaviour
{
    PlayerManager playerManager;

    [Header("DATA AI")]
    [SerializeField] float minIdleTime;
    [SerializeField] float maxIdleTime;
    [SerializeField] float minAwareTime;
    [SerializeField] float maxAwareTime;

    [Header("STATUS")]
    [SerializeField] float currentIdleTime;
    [SerializeField] float currentAwareTime;



    public NPC_State states;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;

        SwitchState(NPC_State.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        StateCondition();
    }

    void StateCondition()
    {
        switch(states)
        {
            case NPC_State.IDLE:
                AIState_Idle();
                break;

            case NPC_State.GO_TO:
                AIState_GoTo();
                break;

            case NPC_State.AWARE:
                AIState_Aware();
                break;

            case NPC_State.CHASING:
                AIState_Chasing();
                break;
        }
    }

    public void SwitchState(NPC_State newState)
    {

        //ON NEW STATE ENTER
        switch(newState)
        {
            case NPC_State.IDLE:
                currentIdleTime = Random.Range(minIdleTime, maxIdleTime);
                break;

            case NPC_State.GO_TO:
                //Decide position to go (room point)
                break;

            case NPC_State.AWARE:
                currentAwareTime = Random.Range(minAwareTime, maxAwareTime);
                break;

            case NPC_State.CHASING:

                break;
        }


        states = newState;
    }

    void AIState_Idle()
    {
        //Selama interval idle sambil lihat kanan kiri
        if(currentIdleTime > 0)
        {
            currentIdleTime -= Time.deltaTime;
        }
        else
        {
            currentIdleTime = 0;

            SwitchState(NPC_State.GO_TO);
        }
    }

    void AIState_GoTo()
    {
        //Vector2 destination = null;
    }

    void AIState_Aware()
    {
        if(currentAwareTime > 0)
        {
            currentAwareTime -= Time.deltaTime;
        }
        else
        {
            //Jika masih aware maka player akan ditangkan dan game over
            currentAwareTime = 0;

            //chasing states
            SwitchState(NPC_State.CHASING);

            //player stunned
            playerManager.PlayerOnChanneling(true);
        }
    }

    void AIState_Chasing()
    {
        

        //animation walk

        //go to player position to catch
        //dotween pro, trasform.position
    }
}