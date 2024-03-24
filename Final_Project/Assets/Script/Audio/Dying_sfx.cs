using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying_sfx : MonoBehaviour
{
    public AudioClip die;
    public AudioSource getHit;

    public static Dying_sfx instance;

    private void Awake()
    {
        instance = this;
        getHit = GetComponent<AudioSource>();
    }

    public void hitDead()
    {
        getHit.PlayOneShot(die);
    }
}
