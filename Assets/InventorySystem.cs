using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    public List<Interactable> interactableList = new List<Interactable>();


    public event Action OnItemUpdated;

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(Interactable item)
    {
        interactableList.Add(item);

        OnItemUpdated?.Invoke();
    }

    public void RemoveItem(Interactable item)
    {
        interactableList.Remove(item);

        OnItemUpdated?.Invoke();
    }
}
