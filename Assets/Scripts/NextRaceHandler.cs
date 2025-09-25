using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class NextRaceHandler : MonoBehaviour
    {
        [Tooltip("Button to start the next race")]
        public Button NextRaceButton;

        [Tooltip("The next scene name to be loaded")]
        public string nextSceneName;
        
        private TransitionAnimator transitionAnimator;

        private void OnEnable()
        {
            NextRaceButton.onClick.AddListener(OnNextRaceButtonClick);
        }

        private void OnDisable()
        {
            NextRaceButton.onClick.RemoveListener(OnNextRaceButtonClick);
        }

        private void Start()
        {
            transitionAnimator = GetComponent<TransitionAnimator>();
        }

        // Show transition animation and Load level 2 scene.
        private void OnNextRaceButtonClick()
        {
            GameEvents.NextRace();
            transitionAnimator.StartFadeOut();
            StartCoroutine(LoadSceneAfterFade());
        }

        IEnumerator LoadSceneAfterFade()
        {
            yield return new WaitForSeconds(1.5f); // Match fade duration
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
