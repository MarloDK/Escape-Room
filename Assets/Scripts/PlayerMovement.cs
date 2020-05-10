using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float gravityForce;

    private CharacterController controller;

    private Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 desiredPos = transform.right * x + transform.forward * z;
        controller.Move(desiredPos * movementSpeed * Time.deltaTime);

        velocity.y += -gravityForce * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
