using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public UnityEvent OnPicked;

    public InteractableData data;

    //public Sprite icon;
    public virtual void Interact()
    {
        Picked();
    }

    void Picked()
    {
        OnPicked?.Invoke();
        InventorySystem.instance.AddItem(data);
        this.gameObject.SetActive(false);
    }
}
