using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream_effect : MonoBehaviour
{
    public AudioClip scream;
    public AudioSource Ghost_scream;

    public static Scream_effect instance;

    private void Awake()
    {
        instance = this;
        Ghost_scream = GetComponent<AudioSource>();
    }

    public void Gscream()
    {
        Ghost_scream.PlayOneShot(scream);
    }
}