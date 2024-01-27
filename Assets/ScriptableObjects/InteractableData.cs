using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable Data", menuName = "Data/Interactable Data")]
public class InteractableData : ScriptableObject
{
    public Sprite icon;

    //public Sprite blankTrapSprite;
    public Sprite buildTrapSprite;

    public List<InteractableData> recepies = new List<InteractableData>();

    public string trapEffectActionToNPC;
    public float stunDuration;
}
