using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    public bool isWinner = false;

    public static CheckWinner instance;

    Music_sfx audioManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Music_sfx>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isWinner = true;
            audioManager.StopBackgroundMusic();

            // Find all objects with the "Stop" tag and disable their audio sources
            GameObject[] stopObjects = GameObject.FindGameObjectsWithTag("Stop");
            foreach (GameObject stopObject in stopObjects)
            {
                AudioSource audioSource = stopObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}
