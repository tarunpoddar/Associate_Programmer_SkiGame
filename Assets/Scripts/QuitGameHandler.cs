using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class QuitGameHandler: MonoBehaviour
    {
        [Tooltip("Button that triggers the QuitGame action.")]
        public Button QuitGameButton;

        private void Start()
        {
            QuitGameButton.onClick.AddListener(QuitGame);
        }

        private static void QuitGame()
        {
            Application.Quit();

            // If running in the Unity Editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
