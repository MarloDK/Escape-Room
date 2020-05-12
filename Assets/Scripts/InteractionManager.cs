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
    [SerializeField]
    private LayerMask pickupLayer;

    private bool canInteract = false;
    private bool interacting = false;
    private string currentInteraction;
    private GameObject interactingObject;

    [Header("UI Elements")]
    [SerializeField]
    private GameObject interactText;
    [SerializeField]
    private GameObject pickupText;
    [SerializeField]
    private GameObject radioUI;
    [SerializeField]
    private GameObject batteryText;

    [Header("References")]
    [SerializeField]
    private Flashlight flashlight;

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
            UpdateInteract(0, hit.collider.gameObject);
            currentInteraction = hit.collider.gameObject.name;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Player is looking at " + hit.collider.gameObject.name);
        } else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactDistance, pickupLayer) && !interacting)
        {
            UpdateInteract(1, hit.collider.gameObject);
            currentInteraction = hit.collider.gameObject.name;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Player is looking at " + hit.collider.gameObject.name);
        }
        else UpdateInteract();

        // UI Handling
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            GetAction(true).SetActive(true);
            if (currentInteraction == "Batteries") Destroy(interactingObject, 0.2f);
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) && interacting)
        {
            GetAction(false).SetActive(false);
        }
    }

    // Overload method, decides which text to display
    private void UpdateInteract(int ID, GameObject currentObj)
    {
        interactingObject = currentObj;
        canInteract = true;
        if (ID == 0)
            interactText.SetActive(true);
        else if (ID == 1)
            pickupText.SetActive(true);
    }

    // No overloads method, clears everything
    private void UpdateInteract()
    {
        pickupText.SetActive(false);
        interactText.SetActive(false);
        canInteract = false;
    }

    private GameObject GetAction(bool trigger)
    {
        interacting = trigger;
        switch (currentInteraction)
        {
            case "Radio":
                UpdateCursor(trigger);
                Debug.Log("Interacted with Radio");
                return radioUI;

            case "Batteries":
                Debug.Log("Interacted with Batteries");
                StartCoroutine(ShowText(3.5f, batteryText));
                StartCoroutine(flashlight.GiveBatteries(0.2f));
                return batteryText;

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

    private IEnumerator ShowText(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
