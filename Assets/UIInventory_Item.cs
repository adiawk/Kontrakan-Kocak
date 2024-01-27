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
            imgIcon.color = new Color(1, 1, 1, 0);
        }
        else
        {
            imgIcon.sprite = item.icon;
            imgIcon.color = new Color(1, 1, 1, 1);
        }
    }

    public void Accomplished()
    {
        accomplised.gameObject.SetActive(true);
    }
}
