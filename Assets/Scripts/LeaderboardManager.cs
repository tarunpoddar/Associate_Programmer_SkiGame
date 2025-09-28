using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LeaderboardManager : MonoBehaviour
    {
        public static string[] FormattedTimes = new string[5];
        private readonly List<float> top5RaceTimes = new List<float>(new float[5]);
        private int currentSceneIndex;
        private void Awake()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            //PlayerPrefs.DeleteAll();
        }

        private void Start()
        {
            CheckIfPrefsSet();
            GetTop5RaceTimes();
        }

        private void OnEnable()
        {
            GameEvents.OnRaceTimerStopped += CheckCurrentRaceTime;
        }

        private void OnDisable()
        {
            GameEvents.OnRaceTimerStopped -= CheckCurrentRaceTime;
        }

        private void GetTop5RaceTimes()
        {
            if (PlayerPrefs.HasKey($"{currentSceneIndex}_topTime1"))
                top5RaceTimes[0] = PlayerPrefs.GetFloat($"{currentSceneIndex}_topTime1");
            if (PlayerPrefs.HasKey($"{currentSceneIndex}_topTime2"))
                top5RaceTimes[1] = PlayerPrefs.GetFloat($"{currentSceneIndex}_topTime2");
            if (PlayerPrefs.HasKey($"{currentSceneIndex}_topTime3"))
                top5RaceTimes[2] = PlayerPrefs.GetFloat($"{currentSceneIndex}_topTime3");
            if (PlayerPrefs.HasKey($"{currentSceneIndex}_topTime4"))
                top5RaceTimes[3] = PlayerPrefs.GetFloat($"{currentSceneIndex}_topTime4");
            if (PlayerPrefs.HasKey($"{currentSceneIndex}_topTime5"))
                top5RaceTimes[4] = PlayerPrefs.GetFloat($"{currentSceneIndex}_topTime5");

            FormatTimesToString();
        }

        public void CheckCurrentRaceTime()
        {
            int scorePosition = int.MaxValue;
            bool highScore = false;

            if (top5RaceTimes.Contains((float)Math.Round(RaceTimer.raceTime, 2)))
            {
                print("Time already exists!! not adding duplicate.");
                return;
            }

            if (RaceTimer.raceTime > top5RaceTimes[top5RaceTimes.Count - 1] && top5RaceTimes[top5RaceTimes.Count - 1] > 0.0001f)
                return;

            // Insert from back.
            for (int i = top5RaceTimes.Count - 1; i >= 0; i--)
            {
                
                if (Math.Round(RaceTimer.raceTime, 2) < Math.Round(top5RaceTimes[i], 2)
                    || top5RaceTimes[i] < 0.0001f)
                {
                    highScore = true;

                    if (i < scorePosition)
                        scorePosition = i;
                }
            }

            if (highScore)
            {
                top5RaceTimes.Insert(scorePosition, (float)Math.Round(RaceTimer.raceTime, 2));
                SetBestTimes();
            }
        }

        private void SetBestTimes()
        {
            for (int i = 0; i < top5RaceTimes.Count; i++)
            {
                PlayerPrefs.SetFloat($"{currentSceneIndex}_topTime{i + 1}", top5RaceTimes[i]);
            }

            FormatTimesToString();
        }

        private void CheckIfPrefsSet()
        {
            for (int i = 1; i <= 5; i++)
            {
                //if we don't have our PlayerPrefs set them up with a default value of 0
                if (!PlayerPrefs.HasKey($"{currentSceneIndex}_topTime{i}"))
                {
                    PlayerPrefs.SetFloat($"{currentSceneIndex}_topTime{i}", 0);
                }
            }
        }

        private void FormatTimesToString()
        {
            // Ensure top5RaceTimes only contains 5 elements
            if (top5RaceTimes.Count > 5)
                top5RaceTimes.RemoveRange(5, top5RaceTimes.Count - 5);

            for (int i = 0; i < 5; i++)
            {
                TimeSpan t = TimeSpan.FromSeconds(top5RaceTimes[i]);
                FormattedTimes[i] = t.ToString("m':'ss':'ff");
            }
        }
    }
}
