using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LeaderboardManager : MonoBehaviour
    {
        public static string[] FormattedTimes = new string[5];
        private readonly List<float> top5RaceTimes = new List<float>(new float[5]);

        private void Awake()
        {
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
            if (PlayerPrefs.HasKey("topTime1"))
                top5RaceTimes[0] = PlayerPrefs.GetFloat("topTime1");
            if (PlayerPrefs.HasKey("topTime2"))
                top5RaceTimes[1] = PlayerPrefs.GetFloat("topTime2");
            if (PlayerPrefs.HasKey("topTime3"))
                top5RaceTimes[2] = PlayerPrefs.GetFloat("topTime3");
            if (PlayerPrefs.HasKey("topTime4"))
                top5RaceTimes[3] = PlayerPrefs.GetFloat("topTime4");
            if (PlayerPrefs.HasKey("topTime5"))
                top5RaceTimes[4] = PlayerPrefs.GetFloat("topTime5");

            FormatTimesToString();
        }

        public void CheckCurrentRaceTime()
        {
            int scorePosition = int.MaxValue;
            bool highScore = false;

            // Insert from back.
            for (int i = top5RaceTimes.Count - 1; i >= 0; i--)
            {
                if (RaceTimer.raceTime < top5RaceTimes[i] || top5RaceTimes[i] < 0.0001f)
                {
                    highScore = true;

                    if (i < scorePosition)
                        scorePosition = i;
                }
            }

            if (highScore)
            {
                top5RaceTimes.Insert(scorePosition, RaceTimer.raceTime);
                SetBestTimes();
            }
        }

        private void SetBestTimes()
        {
            for (int i = 0; i < top5RaceTimes.Count; i++)
            {
                PlayerPrefs.SetFloat($"topTime{i + 1}", top5RaceTimes[i]);
            }

            FormatTimesToString();
        }

        private static void CheckIfPrefsSet()
        {
            for (int i = 1; i <= 5; i++)
            {
                //if we don't have our PlayerPrefs set them up with a default value of 0
                if (!PlayerPrefs.HasKey($"topTime{i}"))
                {
                    PlayerPrefs.SetFloat($"topTime{i}", 0);
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
                FormattedTimes[i] = top5RaceTimes[i].ToString("F2");
            }
        }
    }
}
