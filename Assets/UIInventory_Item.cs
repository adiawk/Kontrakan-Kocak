using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory_Item : MonoBehaviour
{
    public Image imgIcon;

    public void Set(Interactable item)
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
}
