using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public bool isCanInteracting;
    [SerializeField] PlayerBallonInteraction playerBallonInteraction;
    [SerializeField] LayerMask interactableLayer;
    public float interactionRange = 2f; // Set the interaction range in the Unity Editor

    void Update()
    {
        if(isCanInteracting)
            DetectInteractableObjects();
    }


    void DetectInteractableObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        bool foundInteractableObject = false;

        foreach (Collider2D collider in hitColliders)
        {
            //Debug.Log($"INI: {collider.gameObject.name}");

            // Check if the object has an Interactable component or tag or any other criteria
            Interactable interactableObject = collider.GetComponent<Interactable>();

            if (interactableObject != null)
            {
                // Show UI prompt
                ShowInteractionPrompt(interactableObject);
                foundInteractableObject = true;

                if (Input.GetKeyDown(KeyCode.E) && isCanInteracting)
                {
                    // Perform interaction with the interactable object
                    interactableObject.Interact();
                }
            }
        }

        // If no interactable objects are found, hide the UI prompt
        if (!foundInteractableObject)
        {
            HideInteractionPrompt();
        }
    }

    void ShowInteractionPrompt(Interactable interacted)
    {
        // Show the UI prompt
        if (playerBallonInteraction != null)
        {
            //interactionPrompt.text = "Press E to interact";
            playerBallonInteraction.ballonGameobject.SetActive(true);
            playerBallonInteraction.Set(interacted.data.icon);
        }
    }

    public void HideInteractionPrompt()
    {
        // Hide the UI prompt
        if (playerBallonInteraction != null)
        {
            playerBallonInteraction.ballonGameobject.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the interaction range in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
