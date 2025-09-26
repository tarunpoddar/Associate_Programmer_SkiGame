using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class RaceTimer : MonoBehaviour
    {
        public bool raceStarted = false;
        public static float raceTime = 0;
        private TimeSpan timePlaying;

        private void OnEnable()
        {
            GameEvents.OnRaceStart += StartTimer;
            GameEvents.OnRaceStop += StopTimer;
        }

        private void OnDisable()
        {
            GameEvents.OnRaceStart -= StartTimer;
            GameEvents.OnRaceStop -= StopTimer;
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
                print("RACE TIME: " + timePlaying.ToString("mm':'ss':'ff"));

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