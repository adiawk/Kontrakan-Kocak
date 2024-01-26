using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory_Item : MonoBehaviour
{
    public Image imgIcon;

    [SerializeField] GameObject accomplised;

    public void Set(InteractableData item)
    {
        if (item == null)
        {
            imgIcon.sprite = null;
        }
        else
        {
            imgIcon.sprite = item.icon;
        }
    }

    public void Accomplished()
    {
        accomplised.gameObject.SetActive(true);
    }
}
