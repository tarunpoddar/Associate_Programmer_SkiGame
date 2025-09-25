using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class RetryGameHandler : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Button to retry the game.")]
        private UnityEngine.UI.Button retryButton;

        private TransitionAnimator transitionAnimator;

        void OnEnable()
        {
            retryButton.onClick.AddListener(OnRetryButtonClick);
        }

        void OnDisable()
        {
            retryButton.onClick.RemoveListener(OnRetryButtonClick);
        }

        private void Start()
        {
            transitionAnimator = GetComponent<TransitionAnimator>();
        }

        private void OnRetryButtonClick()
        {
            GameEvents.RetryGame();
            transitionAnimator.StartFadeOut();
            StartCoroutine(ReloadAfterDelay());
        }

        private static IEnumerator ReloadAfterDelay()
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
