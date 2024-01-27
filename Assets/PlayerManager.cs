using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    CharacterMovement move;
    CharacterInteraction interaction;
    public RoomIdentifier roomIdentifier;
    public CharacterAnimation anim;
    public CharacterHideAbility hideAbility;

    public bool isChannelling;

    private void Awake()
    {
        instance = this;

        TryGetComponent(out move);
        TryGetComponent(out interaction);
        TryGetComponent(out roomIdentifier);
        TryGetComponent(out anim);
        TryGetComponent(out hideAbility);
    }

    public void PlayerOnChanneling(bool isChanneling)
    {
        TogglePlayerMove(!isChanneling);
        TogglePlayerInteraction(!isChanneling);

        isChannelling = isChanneling;
    }

    public void TogglePlayerMove(bool isAbleToMove)
    {
        move.isMoveable = isAbleToMove;
    }

    public void TogglePlayerInteraction(bool isAbleToInteract)
    {
        interaction.isCanInteracting = isAbleToInteract;

        if(!isAbleToInteract)
        {
            interaction.HideInteractionPrompt();
        }
    }
}
