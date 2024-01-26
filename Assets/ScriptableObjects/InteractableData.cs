using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable Data", menuName = "Data/Interactable Data")]
public class InteractableData : ScriptableObject
{
    public Sprite icon;

    public List<InteractableData> recepies = new List<InteractableData>();

}
