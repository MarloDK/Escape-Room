using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private float interactDistance;
    [SerializeField]
    private LayerMask interactableLayer;

    private bool canInteract = false;
    private bool interacting = false;
    private string currentInteraction;

    [Header("UI Elements")]
    [SerializeField]
    private GameObject interactText;
    [SerializeField]
    private GameObject radioUI;

    private MouseLook mouseController;
    private PlayerMovement playerController;

    private void Start()
    {
        mouseController = GetComponent<MouseLook>();
        playerController = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        // Player interaction handling
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactDistance, interactableLayer) && !interacting)
        {
            UpdateInteract(true);
            currentInteraction = hit.collider.gameObject.name;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Player is looking at " + hit.collider.gameObject.name);
        }
        else UpdateInteract(false);

        // UI Handling
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            GetAction(true).SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) && interacting)
        {
            GetAction(false).SetActive(false);
        }
    }

    private void UpdateInteract(bool boolean)
    {
        interactText.SetActive(boolean);
        canInteract = boolean;
    }

    private GameObject GetAction(bool trigger)
    {
        UpdateCursor(trigger);
        interacting = trigger;
        switch (currentInteraction)
        {
            case "Radio":
                Debug.Log("Interacted with Radio");
                return radioUI;

            default:
                Debug.Log("InteractionManager::GetAction():: Invalid object name");
                return null;
        }
    }

    private void UpdateCursor(bool trigger)
    {
        if (trigger)
            Cursor.lockState = CursorLockMode.None;
        else if (!trigger)
            Cursor.lockState = CursorLockMode.Locked;

        mouseController.enabled = !trigger;
        playerController.enabled = !trigger;
    }
}
