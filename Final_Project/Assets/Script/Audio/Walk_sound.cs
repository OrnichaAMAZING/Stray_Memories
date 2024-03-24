using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk_sound : MonoBehaviour
{

    public List<AudioClip> playerWalking;
    public AudioClip playerRunning;
    public AudioSource playerSource;

    public int pos;

    public static Walk_sound instance;
    private void Awake()
    {
        instance = this;
        playerSource = GetComponent<AudioSource>();
    }

    public void playWalking()
    {
        pos = (int)Mathf.Floor(Random.Range(0, playerWalking.Count));
        playerSource.PlayOneShot(playerWalking[pos]);
    }

    public void playRunning()
    {
        playerSource.PlayOneShot(playerRunning);
    }

}
