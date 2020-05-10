using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioBehaviour : MonoBehaviour
{
    private TMP_InputField stationInput;
    private string currentStationString;
    private double currentStationFrequency;

    private void Start()
    {
        StartCoroutine(DebugCurrentStation());
        stationInput = GetComponent<TMP_InputField>();
    }

    public void OnStationEnter()
    {
        switch (stationInput.text)
        {
            case "666":
                currentStationString = "Devil368";
                currentStationFrequency = 666;
                StartCoroutine(GiveFeedback("Station Set"));
                break;

            case "104.3":
                currentStationString = "NovaHell";
                currentStationFrequency = 104.3;
                StartCoroutine(GiveFeedback("Station Set"));
                break;

            case "99.2":
                currentStationString = "Hell3";
                currentStationFrequency = 99.2;
                StartCoroutine(GiveFeedback("Station Set"));
                break;

            case "503.2":
                currentStationString = "Ending";
                currentStationFrequency = 503.2;
                StartCoroutine(GiveFeedback("Station Set"));
                break;

            default:
                StartCoroutine(GiveFeedback("Invalid Station"));
                break;
        }
    }

    private IEnumerator DebugCurrentStation()
    {
        while (true)
        {
            Debug.Log("Current Station: " + currentStationString);
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator GiveFeedback(string message)
    {
        stationInput.text = message;
        yield return new WaitForSeconds(2.25f);

        if (currentStationFrequency != 0)
            stationInput.text = currentStationFrequency.ToString();
        else
            stationInput.text = "Enter station frequency";
    }
}
