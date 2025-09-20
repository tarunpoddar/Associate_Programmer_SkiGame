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

        [Tooltip("Sound played when the player passes incorrectly through a gate.")]
        public AudioClip incorrectSound;

        AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            PlayerEvents.OnPlayerHit += PlaySound;
            GameEvents.OnCorrectPass += PlayCorrectPassSound;
            GameEvents.OnIncorrectPass += PlayIncorrectPassSound;
            GameEvents.OnRaceStart += PlayStartSound;
            GameEvents.OnRaceOver += PlayStartSound;
        }


        private void OnDisable()
        {
            PlayerEvents.OnPlayerHit -= PlaySound;
            GameEvents.OnRaceStart -= PlayStartSound;
            GameEvents.OnRaceOver -= PlayStartSound;
        }

        private void PlaySound(GameObject hitObject)
        {
            Debug.Log($"Player hit {hitObject.tag}");

            if (hitObject.CompareTag("Tree"))
            {
                audioSource.PlayOneShot(treeHitSound);
                return;
            }

            if (hitObject.CompareTag("Border"))
            {
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
