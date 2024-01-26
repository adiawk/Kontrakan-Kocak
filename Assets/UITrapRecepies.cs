using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrapRecepies : MonoBehaviour
{
    [SerializeField] InteractableTrap trap;
    [SerializeField] UIInventory_Item[] uiRecepies;

    public void InitializeUIRecepies()
    {
        for (int i = 0; i < uiRecepies.Length; i++)
        {
            try
            {
                uiRecepies[i].gameObject.SetActive(true);
                uiRecepies[i].Set(trap.trapRecepies[i].data);
            }
            catch
            {
                uiRecepies[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < uiRecepies.Length; i++)
        {
            if (uiRecepies[i].isActiveAndEnabled)
            {
                try
                {
                    if (trap.trapRecepies[i].isAccomplished)
                    {
                        uiRecepies[i].Accomplished();
                    }
                }
                catch { };
            }
        }
    }
}
