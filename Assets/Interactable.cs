using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Sprite icon;
    public virtual void Interact()
    {
        //Debug.Log("INTERACTED: INVENTORY");

        InventorySystem.instance.AddItem(this);

        this.gameObject.SetActive(false);

    }
}
