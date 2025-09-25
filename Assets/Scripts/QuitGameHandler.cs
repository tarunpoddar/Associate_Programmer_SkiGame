using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class QuitGameHandler : MonoBehaviour
    {
        [Tooltip("Button that triggers the QuitGame action.")]
        public Button QuitGameButton;

        [Tooltip("Panel that contains the QuitGame confirmation dialog.")]
        public Button QuitGameOkButton;

        [Tooltip("Button that cancels the QuitGame action.")]
        public Button QuitGameCancelButton;

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

        private static void QuitGame()
        {
            GameEvents.InvokeQuitGame();
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
            GameEvents.InvokeQuitGameCancel();
        }
    }
}
