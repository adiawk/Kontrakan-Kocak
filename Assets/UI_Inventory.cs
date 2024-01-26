using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    InventorySystem InventorySystem;

    public UIInventory_Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        InventorySystem = InventorySystem.instance;

        InventorySystem.OnItemUpdated += InventorySystem_OnItemUpdated;

        InventorySystem_OnItemUpdated();
    }

    private void InventorySystem_OnItemUpdated()
    {
        for (int i = 0; i < items.Length; i++)
        {
            try
            {
                items[i].Set(InventorySystem.interactableList[i]);
            }
            catch
            {
                items[i].Set(null);
            }
            
        }
    }

}