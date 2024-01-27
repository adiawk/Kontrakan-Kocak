using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG;

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
    Vector3 goToDestination;
    float randomMaxAwareTime;
    [SerializeField] float currentIdleTime;
    [SerializeField] float currentAwareTime;

    public float speed;

    [SerializeField] GameObject parentAwareProgress;
    [SerializeField] UIProgressBar progressBar;


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
        flagMovingChasing = false;
        flagMovingGoto = false;

        //ON NEW STATE ENTER
        switch (newState)
        {
            case NPC_State.IDLE:
                currentIdleTime = Random.Range(minIdleTime, maxIdleTime);
                parentAwareProgress.gameObject.SetActive(false);
                break;

            case NPC_State.GO_TO:

                parentAwareProgress.gameObject.SetActive(false);
                //Decide position to go (room point)
                goToDestination = RoomManager.instance.GetAvailableReadyTrap;
                break;

            case NPC_State.AWARE:
                parentAwareProgress.gameObject.SetActive(true);
                randomMaxAwareTime = Random.Range(minAwareTime, maxAwareTime);
                currentAwareTime = randomMaxAwareTime;
                break;

            case NPC_State.CHASING:
                
                break;
        }

        Debug.Log($"STATE: {newState}");

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

    bool flagMovingGoto = false;
    void AIState_GoTo()
    {
        //Vector2 destination = null;
        if (!flagMovingGoto)
        {
            flagMovingGoto = true;

            transform.DOMoveX(goToDestination.x, speed).SetSpeedBased()
                .OnComplete(()=>SwitchState(NPC_State.IDLE));
        }
    }

    void AIState_Aware()
    {
        bool isPlayerVisible = true;
        if(isPlayerVisible)
        {
            if (currentAwareTime > 0)
            {
                currentAwareTime -= Time.deltaTime;

                float progress = Mathf.Clamp01(currentAwareTime / randomMaxAwareTime);

                progressBar.SetProgress(1f - progress);
            }
            else
            {
                //Jika masih aware maka player akan ditangkan dan game over
                currentAwareTime = 0;

                //chasing states
                SwitchState(NPC_State.CHASING);

                Debug.Log("CHASING");

                //player stunned
                playerManager.PlayerOnChanneling(true);
            }
        }
        else
        {
            SwitchState(NPC_State.IDLE);
        }
        
    }

    bool flagMovingChasing = false;
    void AIState_Chasing()
    {
        //animation walk

        if (!flagMovingChasing)
        {
            flagMovingChasing = true;
            Vector2 playerPos = playerManager.transform.position;
            //go to player position to catch
            transform.DOMoveX(playerPos.x, speed * 1.5f).SetSpeedBased()
                .OnComplete(() => GameManager.instance.GameOver()); 
        }   
    }
}