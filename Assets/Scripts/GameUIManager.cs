using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

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

        [Tooltip("Assign the TotalRaceCompletedText object.")]
        public TextMeshProUGUI TotalRaceCompletedText;

        [Tooltip("Assign the TimeDataTexts object.")]
        public TextMeshProUGUI[] TimeDataTexts;

        [Tooltip("Text to display loading progress.")]
        public TextMeshProUGUI LoadingText;

        [Tooltip("Text to display on race failed.")]
        public CanvasGroup RaceFailedText;

        [Tooltip("Assign the Next Race button.")]
        public Button NextRaceButton;

        private void Awake()
        {
            RaceOverPanel.SetActive(false);
            MessageText.gameObject.SetActive(false);
            LoadingText.text = "Loading...";
            LoadingText.gameObject.SetActive(false);
            TotalRaceCompletedText.gameObject.SetActive(false);
            RaceFailedText.DOFade(0, 0f);
        }

        void OnEnable()
        {
            GameEvents.OnRaceStop += HandleRaceOver;
            GameEvents.OnIncorrectPass += HandleIncorrectPass;
            GameEvents.OnBonusPass += HandleBonusPass;
            GameEvents.OnRetryRace += HandleRetryRace;
            GameEvents.OnNextLevel += HandleNextLevel;
            GameEvents.OnLeaderboardUpdated += OnLeaderBoardUpdated;
            GameEvents.OnPlayerDied += HandlePlayerDied;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            GameEvents.OnRaceStop -= HandleRaceOver;
            GameEvents.OnIncorrectPass -= HandleIncorrectPass;
            GameEvents.OnBonusPass -= HandleBonusPass;
            GameEvents.OnRetryRace -= HandleRetryRace;
            GameEvents.OnNextLevel -= HandleNextLevel;
            GameEvents.OnLeaderboardUpdated -= OnLeaderBoardUpdated;
            GameEvents.OnPlayerDied -= HandlePlayerDied;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void HandleBonusPass()
        {
            MessageText.color = Color.green;
            StartCoroutine(ShowMessage("Bonus Pass! -1s"));
        }

        private void HandleIncorrectPass()
        {
            MessageText.color = Color.red;
            StartCoroutine(ShowMessage("Incorrect Pass! +3s"));
        }

        private IEnumerator ShowMessage(string message)
        {
            MessageText.gameObject.SetActive(true);
            MessageText.text = message;
            yield return new WaitForSeconds(2);
            MessageText.gameObject.SetActive(false);
        }

        private void Update()
        {
            RaceTimeText.text = $"Time: {TimeSpan.FromSeconds(Math.Round(RaceTimer.raceTime, 2)).ToString("m':'ss':'ff")} sec";
        }

        private void HandleRaceOver()
        {
            RaceDataManager.Instance.IncrementRaceCount();

            FinalRaceTimeText.text = $"Race Time: {TimeSpan.FromSeconds(Math.Round(RaceTimer.raceTime, 2)).ToString("m':'ss':'ff")} sec";
            TotalRaceCompletedText.text = $"Races Completed: {RaceDataManager.Instance.GetTotalRacesCompleted().ToString()}";
            TotalRaceCompletedText.gameObject.SetActive(true);

            RaceOverPanel.SetActive(true);
            RaceActivePanel.SetActive(false);
        }

        private void HandlePlayerDied()
        {
            RaceFailedText.DOFade(1, 0.5f);
            NextRaceButton.gameObject.SetActive(false);
        }

        private void HandleNextLevel()
        {
            HandleRetryRace();
        }

        private void HandleRetryRace()
        {
            RaceOverPanel.SetActive(false);
            LoadingText.gameObject.SetActive(true);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadingText.gameObject.SetActive(false);
        }

        private void OnLeaderBoardUpdated()
        {
            // Update the leaderboard UI here
            string[] times = LeaderboardManager.FormattedTimes;

            Debug.Log("Updating leaderboard UI with times:");

            for (int i = 0; i < times.Length; i++)
            {
                TimeDataTexts[i].text = times[i];

                if (FinalRaceTimeText.text.ToString().Contains(times[i]))
                {
                    TimeDataTexts[i].DOColor(new Color(1f, 0.7597771f, 0f), 0.3f);
                    TimeDataTexts[i].DOFade(0.5f, 0.3f).SetLoops(-1, LoopType.Yoyo);
                }
            }
        }
    }
}
