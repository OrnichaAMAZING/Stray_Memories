using UnityEngine;

public class ItemAudio : MonoBehaviour
{
    public AudioClip itemSound; // The audio clip for the item
    public AudioSource audioSource; // Reference to the AudioSource component
    public Transform player; // Reference to the player's transform
    public float maxDistance = 10f; // The maximum distance at which the item audio can be heard
    public float maxVolume = 1f; // The maximum volume of the item audio

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the player and the item
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Calculate the volume based on the distance
        float volume = Mathf.Clamp01(1 - (distanceToPlayer / maxDistance)) * maxVolume;

        // Set the volume of the audio source
        audioSource.volume = volume;

        // Play the item sound if the player is close enough
        if (distanceToPlayer <= maxDistance)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = itemSound;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop(); // Stop the audio if the player is too far away
        }
    }
}