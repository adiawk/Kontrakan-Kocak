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
    CHASING,
    STUNNED
}

public class NPC_AI : MonoBehaviour
{
    public static NPC_AI instance;
    [SerializeField] Transform visualNPC;

    RoomIdentifier roomIdentifier;
    PlayerManager playerManager;
    NPC_Detection NPC_Detection;
    CharacterAnimation anim;
    public NPC_Trapped trapped;

    [Header("DATA AI")]
    [SerializeField] float minIdleTime;
    [SerializeField] float maxIdleTime;
    [SerializeField] float minAwareTime;
    [SerializeField] float maxAwareTime;

    [Header("STATUS")]
    Vector3 goToDestination;
    InteractableTrap trapDestination;

    float randomMaxAwareTime;
    [SerializeField] float currentIdleTime;
    [SerializeField] float currentAwareTime;

    public float speed;

    [SerializeField] GameObject parentAwareProgress;
    [SerializeField] UIProgressBar progressBar;


    [HideInInspector] public float StunDuration;
    public NPC_State states;

    private void Awake()
    {
        instance = this;
        TryGetComponent(out trapped);
        TryGetComponent(out anim);
        TryGetComponent(out roomIdentifier);
        TryGetComponent(out NPC_Detection);
    }
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
                anim.SetWalk(false);
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
                anim.SetWalk(false);
                break;

            case NPC_State.CHASING:
                anim.SetWalk(true);
                break;

            case NPC_State.STUNNED:
                StartCoroutine(BackToIdle());
                anim.SetWalk(false);
                break;
        }

        //Debug.Log($"STATE: {newState}");

        states = newState;
    }

    IEnumerator BackToIdle()
    {
        UI_PlayerHUD.instance.HideUI();

        yield return new WaitForSeconds(StunDuration);
        UI_PlayerHUD.instance.ShowUI();
        RoomManager.instance.DisableSecondaryVCAM();
        SwitchState(NPC_State.IDLE);

        GameManager.instance.CheckGameCondition();
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
    float latestXPosition;
    void AIState_GoTo()
    {

        //Vector2 destination = null;
        //if (!flagMovingGoto)
        //{
        //    flagMovingGoto = true;

        //    //transform.DOMoveX(goToDestination.x, speed).SetSpeedBased()
        //    //    .OnComplete(()=>SwitchState(NPC_State.IDLE));



        //}

        float destinationX = goToDestination.x;
        float arrivalThreshold = 1f;

        // Calculate the distance to the destination along the X-axis
        float distanceToDestination = Mathf.Abs(destinationX - transform.position.x);

        // Check if the distance to the destination is greater than the arrival threshold
        if (distanceToDestination > arrivalThreshold)
        {
            // Calculate the direction to the destination
            float direction = Mathf.Sign(destinationX - transform.position.x);

            // Calculate the movement along the X-axis
            float movement = direction * speed * Time.deltaTime;

            // Update the position to move towards the destination along the X-axis
            transform.Translate(new Vector3(movement, 0, 0));

            if (movement > 0)
            {
                visualNPC.localScale = new Vector3(1, 1, 1);
                anim.SetWalk(true);
            }
            else if (movement < 0)
            {
                visualNPC.localScale = new Vector3(-1, 1, 1);
                anim.SetWalk(true);
            }
            else
            {
                anim.SetWalk(false);
            }
        }
        else
        {
            // If the distance is less than the arrival threshold, consider it has arrived
            //Debug.Log("Destination reached!");

            //IDLE BALIK (KALO DESTINATION NYA ROOM)
            SwitchState(NPC_State.IDLE);

        }
    }

    void AIState_Aware()
    {
        anim.SetWalk(false);

        bool isPlayerVisible = playerManager.hideAbility.isVisible;
        bool isInSameRoom = true/*roomIdentifier.currentRoom == playerManager.roomIdentifier.currentRoom*/;
        bool isInRange = NPC_Detection.IsInAwareRangeX;
        // Check if the distance between this object's X position and NPC's X position is lower than the threshold
        //float distance = Mathf.Abs(transform.position.x - playerManager.transform.position.x);
        //isInRange = distance < 8f;

        if (isPlayerVisible && isInSameRoom && isInRange)
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