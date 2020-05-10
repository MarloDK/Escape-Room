using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    private GameObject lightSource;

    private bool lightActive = false;

    private void Start()
    {
        lightSource.SetActive(lightActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lightSource.SetActive(!lightActive);
            lightActive = !lightActive;
        }
    }
}
