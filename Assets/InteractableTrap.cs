using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTrap : Interactable
{
    [HideInInspector] public bool isFinishBuild;

    [SerializeField] UITrapRecepies uiTrapRecepies;

    [HideInInspector] public List<TrapRecepiesStatus> trapRecepies = new List<TrapRecepiesStatus>();

    bool isBuilding;
    [SerializeField] float buildTime;

    float currentBuildTime;

    public UnityEvent OnRecepiesUpdated;
    public UnityEvent OnStartBuilding;
    public UnityEvent<float> OnBuildingProgress;
    public UnityEvent OnFinishedBuilding;


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
        uiTrapRecepies.InitializeUIRecepies();
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
        OnFinishedBuilding?.Invoke();

        PlayerManager.instance.PlayerOnChanneling(false);
    }
}

[System.Serializable]
public class TrapRecepiesStatus
{
    public InteractableData data;
    public bool isAccomplished;
}