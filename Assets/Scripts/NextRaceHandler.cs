using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class NextRaceHandler : MonoBehaviour
    {
        [Tooltip("Button to start the next race")]
        public Button NextRaceButton;

        private void Start()
        {
            NextRaceButton.onClick.AddListener(LoadNextRace);
        }

        // Unload Level1_Scene and load Level2_Scene
        private static void LoadNextRace()
        {
            SceneManager.LoadScene("Level2_Scene");
        }
    }
}
