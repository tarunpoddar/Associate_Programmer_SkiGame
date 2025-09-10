using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Tooltip("Sound to play when the player character is hit")]
    public AudioClip collisionSound;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerEvents.OnPlayerHit += playCollisionSound;
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerHit -= playCollisionSound;
    }

    private void playCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
