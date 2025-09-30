using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayGameHandler : MonoBehaviour
    {
        [Tooltip("Button to start the game")]
        public Button PlayButton;

        [Tooltip("Panel start game container")]
        public GameObject StartGamePanel;

        [Tooltip("The next scene name to be loaded")]
        public string nextSceneName;
        
        [Tooltip("Text to display loading progress.")]
        public TextMeshProUGUI loadingText;
        
        private TransitionAnimator transitionAnimator;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            loadingText.gameObject.SetActive(false);
            transitionAnimator = GetComponent<TransitionAnimator>();
        }

        private void OnEnable() 
        {
            PlayButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable()
        {
            PlayButton.onClick.RemoveListener(OnPlayButtonClick);
        }

        // Show transition animation and Load level 2 scene.
        private void OnPlayButtonClick()
        {
            transitionAnimator.StartFadeOut();

            StartGamePanel.SetActive(false);
            loadingText.gameObject.SetActive(true);
            audioSource.Stop();

            StartCoroutine(LoadSceneAfterFade());
        }

        IEnumerator LoadSceneAfterFade()
        {
            yield return new WaitForSeconds(1.5f); // Match fade duration
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
