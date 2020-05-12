using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    private GameObject lightSource;

    private bool powered = false;
    private bool lightActive = false;

    private void Start()
    {
        lightSource.SetActive(lightActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && powered)
        {
            lightSource.SetActive(!lightActive);
            lightActive = !lightActive;
        }
    }

    public IEnumerator GiveBatteries(float time)
    {
        yield return new WaitForSeconds(time);
        powered = true;
    }
}
