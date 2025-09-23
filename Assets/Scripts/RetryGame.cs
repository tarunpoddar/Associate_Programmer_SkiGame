using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class RetryGame : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Button to retry the game.")]
        private UnityEngine.UI.Button retryButton;

        [SerializeField]
        [Tooltip("Text to display loading progress.")]
        private TextMeshProUGUI loadingText;

        void Awake()
        {
            retryButton.onClick.AddListener(OnRetryButtonClick);
            loadingText.text = "Loading...";
            loadingText.gameObject.SetActive(false);
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnRetryButtonClick()
        {
            GameEvents.RetryGame();
            // Optionally delay the reload to allow destruction effects
            StartCoroutine(ReloadAfterDelay(1f));
        }

        private IEnumerator ReloadAfterDelay(float delay)
        {
            loadingText.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            loadingText.gameObject.SetActive(false);
        }
    }
}
