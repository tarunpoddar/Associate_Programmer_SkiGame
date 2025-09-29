using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StartScreenSounds : MonoBehaviour
    {
        [Tooltip("Button that toggles the mute state.")]
        public GameObject MuteSoundButton;

        [Tooltip("Button that toggles the unmute state.")]
        public GameObject UnmuteSoundButton;

        [Tooltip("Sound to play on start screen.")]
        public AudioClip StartScreenSound;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            
            MuteSoundButton.SetActive(false);
            UnmuteSoundButton.SetActive(true);

            MuteSoundButton.GetComponent<Button>().onClick.AddListener(MuteSound);
            UnmuteSoundButton.GetComponent<Button>().onClick.AddListener(UnmuteSound);

            audioSource.PlayOneShot(StartScreenSound);
        }

        private void MuteSound()
        {
            audioSource.UnPause();
            MuteSoundButton.SetActive(false);
            UnmuteSoundButton.SetActive(true);
            Debug.Log("Sound Unmuted.");
        }

        private void UnmuteSound()
        {
            audioSource.Pause();
            MuteSoundButton.SetActive(true);
            UnmuteSoundButton.SetActive(false);
            Debug.Log("Sound Muted.");
        }
    }
}
