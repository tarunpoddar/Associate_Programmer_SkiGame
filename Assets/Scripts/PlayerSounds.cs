using System;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Manages and plays audio clips associated with player actions and events.
    /// </summary>
    /// <remarks>This class is responsible for handling audio playback for specific player-related events,
    /// such as collisions with obstacles. It subscribes to relevant events and plays the appropriate sound effects
    /// using an <see cref="AudioSource"/> component.</remarks>
    public class PlayerSounds : MonoBehaviour
    {
        [Tooltip("Sound played when the player hits an obstacle.")]
        public AudioClip obstacleHitSound;

        [Tooltip("Sound played when the player starts skiing.")]
        public AudioClip SkiSound;

        [Tooltip("Sound played when the player jumps")]
        public AudioClip jumpSound;

        [Tooltip("Sound played when the game starts.")]
        public AudioClip startSound;

        [Tooltip("Sound played when the player hits a tree.")]
        public AudioClip treeHitSound;

        [Tooltip("Sound played when the player hits snow ball or snowman.")]
        public AudioClip snowHitSound;

        [Tooltip("Sound played when the player hits the borders.")]
        public AudioClip borderHitSound;

        [Tooltip("Sound played when the player passes correctly through a gate.")]
        public AudioClip correctSound;

        [Tooltip("Sound played when the player passes through a bonus gate.")]
        public AudioClip bonusPassSound;

        [Tooltip("Sound played when the player passes incorrectly through a gate.")]
        public AudioClip incorrectSound;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                Debug.LogError("AudioSource component not found on the GameObject.");
            }
        }

        private void OnEnable()
        {
            PlayerEvents.OnPlayerHit += PlaySound;
            GameEvents.OnCorrectPass += PlayCorrectPassSound;
            GameEvents.OnBonusPass += PlayBonusPassSound;
            GameEvents.OnIncorrectPass += PlayIncorrectPassSound;
            GameEvents.OnRaceStart += PlayStartSound;
            GameEvents.OnRaceStop += PlayStartSound;
        }

        private void OnDisable()
        {
            PlayerEvents.OnPlayerHit -= PlaySound;
            GameEvents.OnRaceStart -= PlayStartSound;
            GameEvents.OnRaceStop -= PlayStartSound;
            GameEvents.OnCorrectPass -= PlayCorrectPassSound;
            GameEvents.OnBonusPass -= PlayBonusPassSound;
            GameEvents.OnIncorrectPass -= PlayIncorrectPassSound;
        }

        private void PlaySound(GameObject hitObject)
        {
            if (hitObject.CompareTag("Tree"))
            {
                audioSource.PlayOneShot(treeHitSound);
                return;
            }

            if (hitObject.CompareTag("Border"))
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                Debug.Log($"{timestamp} Playing border hit sound.");
                audioSource.PlayOneShot(borderHitSound);
                return;
            }

            if (hitObject.CompareTag("Snowman") || hitObject.CompareTag("Snowball"))
            {
                audioSource.PlayOneShot(snowHitSound);
                return;
            }

            audioSource.PlayOneShot(obstacleHitSound);
        }

        private void PlayCorrectPassSound()
        {
            audioSource.PlayOneShot(correctSound);
        }

        private void PlayBonusPassSound()
        {
            audioSource.PlayOneShot(bonusPassSound);
        }

        private void PlayIncorrectPassSound()
        {
            audioSource.PlayOneShot(incorrectSound);
        }

        public void PlaySkiSound()
        {
            audioSource.PlayOneShot(SkiSound);
        }

        public void PlayJumpSound()
        {
            audioSource.PlayOneShot(jumpSound);
        }

        public void PlayStartSound()
        {
            audioSource.PlayOneShot(startSound);
        }
    }
}
