using UnityEngine;

/// <summary>
/// Manages the playback of audio effects for player-related events.
/// </summary>
/// <remarks>This class is responsible for playing specific sounds, such as the collision sound,  in response to
/// player events. It subscribes to relevant events during its lifecycle  and ensures the appropriate audio is played
/// when those events occur.</remarks>
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
