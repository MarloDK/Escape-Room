using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSoundController : MonoBehaviour
{
    private AudioSource audioPlayer;
    private float timeBtwFiring = 5f;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        StartCoroutine(PlayIdleSound());
    }

    private IEnumerator PlayIdleSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBtwFiring);
            audioPlayer.volume = Random.Range(0.2f, 0.33f);
            audioPlayer.Play();
            timeBtwFiring = Random.Range(25f, 100f);
        }
    }
}
