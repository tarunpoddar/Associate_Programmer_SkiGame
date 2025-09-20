using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Manages the timing of a race, including starting, stopping, and applying penalties.
    /// </summary>
    /// <remarks>The <see cref="RaceTimer"/> class tracks the elapsed time of a race and allows for manual
    /// adjustments through penalties. It listens to game events to start and stop the race, as well as to apply
    /// penalties for incorrect actions. The timer uses a combination of a stopwatch and a manual offset to calculate
    /// the total race time.</remarks>
    public class RaceTimer : MonoBehaviour
    {
        [Tooltip("Time penalty in seconds for incorrect passes.")]
        public float timePenalty = 3f;

        private float raceTime;
        private float timeOffset; // Accumulates manual adjustments
        private bool raceActive = false;
        private Stopwatch watch;

        private void Awake()
        {
            watch = new Stopwatch();
            timeOffset = 0f;
        }

        private void OnEnable()
        {
            GameEvents.OnRaceStart += StartRace;
            GameEvents.OnRaceOver += StopRace;
            GameEvents.OnIncorrectPass += AddSeconds;
        }

        private void OnDisable()
        {
            GameEvents.OnRaceStart -= StartRace;
            GameEvents.OnRaceOver -= StopRace;
            GameEvents.OnIncorrectPass -= AddSeconds;
        }

        /// <summary>
        /// Starts the race and begins tracking elapsed time.
        /// </summary>
        /// <remarks>This method initializes the race state, resets the timer, and starts tracking time
        /// from zero.  It also logs a message indicating that the race has started.</remarks>
        public void StartRace()
        {
            raceActive = true;
            watch.Reset();
            watch.Start();
            timeOffset = 0f;
            UnityEngine.Debug.Log("Race started. Timer is running.");
        }

        /// <summary>
        /// Stops the currently active race and calculates the total elapsed time.
        /// </summary>
        /// <remarks>This method stops the race timer, calculates the total race time in seconds,  and
        /// logs the result. The race must be active for this method to have an effect.</remarks>
        public void StopRace()
        {
            raceActive = false;
            watch.Stop();
            raceTime = watch.ElapsedMilliseconds / 1000f + timeOffset;

            UnityEngine.Debug.Log($"Race stopped. Total time: {raceTime:F2} seconds");
        }

        /// <summary>
        /// Adds a time penalty, in seconds, to the current time offset.
        /// </summary>
        /// <remarks>This method increments the internal time offset by the value of the time penalty. It
        /// is typically used to apply a delay or penalty in time-based operations.</remarks>
        public void AddSeconds()
        {
            timeOffset += timePenalty;
            UnityEngine.Debug.Log($"Added {timePenalty:F0} seconds, current race time : {raceTime:F2}");
        }

        private void Update()
        {
            if (raceActive)
            {
                raceTime = watch.ElapsedMilliseconds / 1000f + timeOffset;
            }
        }
    }
}
