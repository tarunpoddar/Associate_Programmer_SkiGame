using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class QuitGameHandler : MonoBehaviour
    {
        [Tooltip("Panel that contains the QuitGame confirmation dialog.")]
        public CanvasGroup QuitGamePanel;

        [Tooltip("Canvas Group for the race over panel to manage its visibility and interactivity.")]
        public CanvasGroup RaceOverPanel;

        [Tooltip("Button that triggers the QuitGame action.")]
        public Button QuitGameButton;

        [Tooltip("Panel that contains the QuitGame confirmation dialog.")]
        public Button QuitGameOkButton;

        [Tooltip("Button that cancels the QuitGame action.")]
        public Button QuitGameCancelButton;

        private void Start()
        {
            QuitGamePanel.DOFade(0, 0f);
            RaceOverPanel.blocksRaycasts = true;
            QuitGamePanel.blocksRaycasts = false;
        }

        private void OnEnable()
        {
            QuitGameButton.onClick.AddListener(QuitGame);
            QuitGameOkButton.onClick.AddListener(QuitGameOk);
            QuitGameCancelButton.onClick.AddListener(QuitGameCancel);
        }

        private void OnDisable()
        {
            QuitGameButton.onClick.RemoveListener(QuitGame);
            QuitGameOkButton.onClick.RemoveListener(QuitGameOk);
            QuitGameCancelButton.onClick.RemoveListener(QuitGameCancel);
        }

        private void QuitGame()
        {
            RaceOverPanel.DOFade(0, 0.5f);
            RaceOverPanel.blocksRaycasts = false;
            QuitGamePanel.DOFade(1, 0.5f);
            QuitGamePanel.blocksRaycasts = true;

            //GameEvents.InvokeQuitGame();// Actual recommended way
        }

        private void QuitGameOk()
        {
            Application.Quit();

            // If running in the Unity Editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void QuitGameCancel()
        {
            RaceOverPanel.DOFade(1, 0.5f);
            RaceOverPanel.blocksRaycasts = true;
            QuitGamePanel.DOFade(0, 0.5f);
            QuitGamePanel.blocksRaycasts = false;
            //GameEvents.InvokeQuitGameCancel();// Actual recommended way
        }
    }
}
