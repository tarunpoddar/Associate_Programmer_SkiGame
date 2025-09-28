using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class RaceTimer : MonoBehaviour
    {
        [Tooltip("Time penalty in seconds for incorrect passes.")]
        public float timePenalty = 3f;
        
        public static float raceTime = 0;

        private bool raceStarted = false;
        private TimeSpan timePlaying;

        private void OnEnable()
        {
            GameEvents.OnRaceStart += StartTimer;
            GameEvents.OnRaceStop += StopTimer;
            GameEvents.OnIncorrectPass += AddPenalty;
        }

        private void OnDisable()
        {
            GameEvents.OnRaceStart -= StartTimer;
            GameEvents.OnRaceStop -= StopTimer;
            GameEvents.OnIncorrectPass -= AddPenalty;
        }

        private void AddPenalty()
        {
            raceTime += timePenalty;
            Debug.Log($"Added {timePenalty:F0} seconds, current race time : {raceTime:F2}");
        }

        private void StartTimer()
        {
            raceTime = 0;
            StartCoroutine("Timer");
            raceStarted = true;
            Debug.Log("Race started. Timer is running.");
        }

        private void StopTimer()
        {
            if (raceStarted)
            {
                StopCoroutine("Timer");
                print("Race Stopped. Total RACE TIME: " + timePlaying.ToString("mm':'ss':'ff"));

                GameEvents.InvokeRaceTimerStopped();
            }
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                raceTime += Time.deltaTime;
                timePlaying = TimeSpan.FromSeconds(raceTime);
                yield return null;
            }
        }
    }
}