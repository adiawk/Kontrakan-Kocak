using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBallonInteraction : MonoBehaviour
{
    public GameObject ballonGameobject;
    [SerializeField] Image iconItem;

    public void Set(Sprite icon)
    {
        iconItem.sprite = icon;
    }
}
