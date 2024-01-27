using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTrap : Interactable
{
    [SerializeField] SpriteRenderer visualSprite;
    [HideInInspector] public bool isFinishBuild;

    [SerializeField] UITrapRecepies uiTrapRecepies;

    [HideInInspector] public List<TrapRecepiesStatus> trapRecepies = new List<TrapRecepiesStatus>();

    bool isBuilding;
    [SerializeField] float buildTime;

    float currentBuildTime;

    public GameObject onTrapDoneSpawnObj;
    public UnityEvent OnRecepiesUpdated;
    public UnityEvent OnStartBuilding;
    public UnityEvent<float> OnBuildingProgress;
    public UnityEvent OnFinishedBuilding;

    public UnityEvent OnTrapHit;

    NPC_AI npc;

    private void Awake()
    {
        for (int i = 0; i < data.recepies.Count; i++)
        {
            trapRecepies.Add(new TrapRecepiesStatus
            {
                data = data.recepies[i],
                isAccomplished = false
            });
        }
    }

    private void Start()
    {
        npc = NPC_AI.instance;
        uiTrapRecepies.InitializeUIRecepies();
    }

    private void Update()
    {
        if(isFinishBuild)
        {
            DetectNPCInRange();
        }
    }

    public override void Interact()
    {
        //base.Interact();
        CollectRecepies();
    }

    void CollectRecepies()
    {
        foreach (var item in trapRecepies)
        {
            if(!item.isAccomplished)
            {
                if (InventorySystem.instance.interactableList.Contains(item.data))
                {
                    InventorySystem.instance.RemoveItem(item.data);
                    item.isAccomplished = true;

                    break;
                }
            }
        }

        OnRecepiesUpdated?.Invoke();

        GetAllRecepies();
    }

    bool IsAllRecepiesAccomplished
    {
        get
        {
            foreach (var item in trapRecepies)
            {
                if(item.isAccomplished == false)
                {
                    return false;
                }
            }
            return true;
        }
    }

    void GetAllRecepies()
    {
        if (IsAllRecepiesAccomplished)
        {
            isBuilding = true;
            currentBuildTime = 0;
            StartCoroutine(BuildingTrap());
            OnStartBuilding?.Invoke();

            PlayerManager.instance.PlayerOnChanneling(true);
        }
    }

    IEnumerator BuildingTrap()
    {
        while(isBuilding)
        {
            if(currentBuildTime < buildTime)
            {
                currentBuildTime += Time.deltaTime;
                float progress = Mathf.Clamp01(currentBuildTime / buildTime);
                OnBuildingProgress?.Invoke(progress);
            }
            else
            {
                isBuilding = false;
            }
            yield return new WaitForEndOfFrame();
        }

        if(currentBuildTime >= buildTime)
        {
            OnFinishedBuild();
        }   
    }

    void OnFinishedBuild()
    {
        isFinishBuild = true;
        SetRealSpriteBuild();
        OnFinishedBuilding?.Invoke();

        PlayerManager.instance.PlayerOnChanneling(false);
    }

    void SetRealSpriteBuild()
    {
        visualSprite.sprite = data.buildTrapSprite;
    }

    public float trapBuildDetectRadius;
    void DetectNPCInRange()
    {
        
        // Check if the distance between this object's X position and NPC's X position is lower than the threshold
        float distance = Mathf.Abs(transform.position.x - npc.transform.position.x);
        float distanceThreshold = trapBuildDetectRadius;

        if (distance < distanceThreshold)
        {
            // Do something when the distance is lower than the threshold
            //Debug.Log("Distance is less than the threshold.");
            npc.trapped.Trapped(data.trapEffectActionToNPC, data.stunDuration);

            if(onTrapDoneSpawnObj != null)
            {
                Instantiate(onTrapDoneSpawnObj, transform.position, Quaternion.identity);
            }
            OnTrapHit?.Invoke();
            RoomManager.instance.trapHitDone++;
            gameObject.SetActive(false);
        }
    }

    // Draw the detection radius in the Scene view using Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, trapBuildDetectRadius);
    }
}

[System.Serializable]
public class TrapRecepiesStatus
{
    public InteractableData data;
    public bool isAccomplished;
}