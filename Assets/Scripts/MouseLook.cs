using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LayerMask))]
public class MouseLook : MonoBehaviour
{
    [Header("Looking")]
    [SerializeField]
    private float mouseSensitivity = 100f;
    [SerializeField]
    private Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = transform.localEulerAngles.x;
    }

    void Update()
    {
        // Player rotation handling
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
