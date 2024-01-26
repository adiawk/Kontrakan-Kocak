using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    public List<InteractableData> interactableList = new List<InteractableData>();


    public event Action OnItemUpdated;

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(InteractableData item)
    {
        interactableList.Add(item);

        OnItemUpdated?.Invoke();
    }

    public void RemoveItem(InteractableData item)
    {
        interactableList.Remove(item);

        OnItemUpdated?.Invoke();
    }
}
