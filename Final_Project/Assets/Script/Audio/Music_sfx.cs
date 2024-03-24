using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_sfx : MonoBehaviour
{
    [Header("----Audio Source------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----Audio SFX------")]
    public AudioClip background;
    public AudioClip background_dead;
    public AudioClip background_win;
    public AudioClip Stun;
    public AudioClip nextPage;
    public AudioClip collect;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

}
