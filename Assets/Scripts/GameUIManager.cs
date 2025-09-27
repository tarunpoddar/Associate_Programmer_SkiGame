using System;
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

        [Tooltip("Panel that appears when the QuitGame button is pressed.")]
        public GameObject QuitGamePanel;

        [Tooltip("Assign the FinalRaceTimeText object.")]
        public TextMeshProUGUI FinalRaceTimeText;

        [Tooltip("Assign the RaceTimeText object.")]
        public TextMeshProUGUI RaceTimeText;

        [Tooltip("Assign the MessageText object.")]
        public TextMeshProUGUI MessageText;

        [Tooltip("Assign the TotalRaceCompletedText object.")]
        public TextMeshProUGUI TotalRaceCompletedText;

        [Tooltip("Assign the TimeDataText object.")]
        public TextMeshProUGUI TimeDataText;

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
            GameEvents.OnRaceStop += HandleRaceOver;
            GameEvents.OnIncorrectPass += HandleIncorrectPass;
            GameEvents.OnRetryRace += HandleRetryRace;
            GameEvents.OnNextLevel += HandleNextLevel;
            GameEvents.OnQuitGame += HandleQuitGame;
            GameEvents.OnQuitGameCancel += HandleQuitGameCancel;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            GameEvents.OnRaceStop -= HandleRaceOver;
            GameEvents.OnIncorrectPass -= HandleIncorrectPass;
            GameEvents.OnRetryRace -= HandleRetryRace;
            GameEvents.OnNextLevel -= HandleNextLevel;
            GameEvents.OnQuitGame -= HandleQuitGame;
            GameEvents.OnQuitGameCancel -= HandleQuitGameCancel;
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
            RaceTimeText.text = $"Time: {TimeSpan.FromSeconds(Math.Round(RaceTimer.raceTime, 2)).ToString("m':'ss':'ff")} sec";
        }

        private void HandleRaceOver()
        {
            FinalRaceTimeText.text = $"Race Time: {TimeSpan.FromSeconds(Math.Round(RaceTimer.raceTime, 2)).ToString("m':'ss':'ff")} sec";
            TotalRaceCompletedText.text = $"Races Completed: {RaceDataManager.Instance.GetTotalRacesCompleted().ToString()}";

            RaceOverPanel.SetActive(true);
            RaceActivePanel.SetActive(false);
        }

        private void FixedUpdate()
        {
            TimeDataText.text = string.Join("\n", LeaderboardManager.FormattedTimes);
        }

        private void HandleNextLevel()
        {
            RaceDataManager.Instance.IncrementRaceCount();
            HandleRetryRace();
        }

        private void HandleRetryRace()
        {
            RaceOverPanel.SetActive(false);
            loadingText.gameObject.SetActive(true);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            loadingText.gameObject.SetActive(false);
        }

        private void HandleQuitGame()
        {
            QuitGamePanel.SetActive(true);
        }

        private void HandleQuitGameCancel()
        {
            QuitGamePanel.SetActive(false);
        }
    }
}
