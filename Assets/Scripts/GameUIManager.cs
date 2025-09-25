using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// Manages the state and UI interactions for a racing game, including race timing,  race completion, and event
    /// handling for incorrect passes.
    /// </summary>
    /// <remarks>This class is responsible for controlling the race-related UI elements, such as  displaying
    /// the race time, handling race completion, and showing messages for  incorrect passes. It listens to game events
    /// to update the UI and manages the  visibility of panels and messages accordingly.</remarks>
    public class GameUIManager : MonoBehaviour
    {
        [Tooltip("Assign the Race over UI panel.")]
        public GameObject RaceOverPanel;

        [Tooltip("Assign the Race Active UI panel.")]
        public GameObject RaceActivePanel;

        [Tooltip("Assign the FinalRaceTimeText object.")]
        public TextMeshProUGUI FinalRaceTimeText;

        [Tooltip("Assign the RaceTimeText object.")]
        public TextMeshProUGUI RaceTimeText;

        [Tooltip("Assign the MessageText object.")]
        public TextMeshProUGUI MessageText;

        [SerializeField]
        [Tooltip("Text to display loading progress.")]
        private TextMeshProUGUI loadingText;

        private void Awake()
        {
            RaceOverPanel.SetActive(false);
            MessageText.gameObject.SetActive(false);
            loadingText.text = "Loading...";
            loadingText.gameObject.SetActive(false);
        }

        void OnEnable()
        {
            GameEvents.OnRaceOver += HandleRaceOver;
            GameEvents.OnIncorrectPass += HandleIncorrectPass;
            GameEvents.OnRetryGame += HandleGameTransition;
            GameEvents.OnNextRace += HandleGameTransition;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            GameEvents.OnRaceOver -= HandleRaceOver;
            GameEvents.OnIncorrectPass -= HandleIncorrectPass;
            GameEvents.OnRetryGame -= HandleGameTransition;
            GameEvents.OnNextRace -= HandleGameTransition;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void HandleIncorrectPass()
        {
            MessageText.color = Color.red;
            StartCoroutine(ShowMessage());
        }

        private IEnumerator ShowMessage()
        {
            MessageText.gameObject.SetActive(true);
            MessageText.text = "Incorrect Pass! +3s";
            yield return new WaitForSeconds(2);
            MessageText.gameObject.SetActive(false);
        }

        private void Update()
        {
            RaceTimeText.text = $"Time: {RaceTimer.raceTime:F0} sec";
        }

        private void HandleRaceOver()
        {
            FinalRaceTimeText.text = $"Race Time: {RaceTimer.raceTime:F2} sec";
            RaceOverPanel.SetActive(true);
            RaceActivePanel.SetActive(false);
        }

        private void HandleGameTransition()
        {
            Debug.Log("Game is being transited..");
            RaceOverPanel.SetActive(false);

            loadingText.gameObject.SetActive(true);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            loadingText.gameObject.SetActive(false);
        }
    }
}
