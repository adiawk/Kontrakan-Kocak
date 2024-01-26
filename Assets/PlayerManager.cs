using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    CharacterMovement move;
    CharacterInteraction interaction;

    private void Awake()
    {
        instance = this;

        TryGetComponent(out move);
        TryGetComponent(out interaction);
    }

    public void PlayerOnChanneling(bool isChanneling)
    {
        TogglePlayerMove(!isChanneling);
        TogglePlayerInteraction(!isChanneling);
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
