using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class QuitGameHandler : MonoBehaviour
    {
        [Tooltip("Panel that contains the QuitGame confirmation dialog.")]
        public GameObject QuitGamePanel;

        [Tooltip("Button that triggers the QuitGame action.")]
        public Button QuitGameButton;

        [Tooltip("Panel that contains the QuitGame confirmation dialog.")]
        public Button QuitGameOkButton;

        [Tooltip("Button that cancels the QuitGame action.")]
        public Button QuitGameCancelButton;

        private void Start()
        {
            QuitGamePanel.SetActive(false);
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
            QuitGamePanel.SetActive(true);// Just for quick testing
            GameEvents.InvokeQuitGame();// Actual recommended way
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
            QuitGamePanel.SetActive(false);// Just for quick testing
            GameEvents.InvokeQuitGameCancel();// Actual recommended way
        }
    }
}
