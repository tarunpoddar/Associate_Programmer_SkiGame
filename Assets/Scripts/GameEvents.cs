namespace Assets.Scripts
{
    /// <summary>
    /// Provides a centralized mechanism for managing and invoking game-related events.
    /// </summary>
    /// <remarks>The <see cref="GameEvents"/> class defines a set of static events and methods for handling
    /// key game actions,  such as starting or ending a race, and tracking correct or incorrect passes.  Subscribers can
    /// attach event handlers to these events to respond to game state changes.</remarks>
    public class GameEvents
    {
        public delegate void RaceStartAction();
        public static event RaceStartAction OnRaceStart;

        public delegate void RaceStopAction();
        public static event RaceStopAction OnRaceStop;

        public delegate void RaceTimerStoppedAction();
        public static event RaceTimerStoppedAction OnRaceTimerStopped;

        public delegate void CorrectPassAction();
        public static event CorrectPassAction OnCorrectPass;

        public delegate void IncorrectPassAction();
        public static event IncorrectPassAction OnIncorrectPass;

        public delegate void RetryRaceAction();
        public static event RetryRaceAction OnRetryRace;

        public delegate void NextLevelAction();
        public static event NextLevelAction OnNextLevel;

        public delegate void QuitGameAction();
        public static event QuitGameAction OnQuitGame;

        public delegate void QuitGameCancelAction();
        public static event QuitGameCancelAction OnQuitGameCancel;

        public static void InvokeRaceStart()
        {
            if (OnRaceStart != null)
                OnRaceStart?.Invoke();
        }

        public static void InvokeRaceStop()
        {
            if (OnRaceStop != null)
                OnRaceStop?.Invoke();
        }

        public static void InvokeCorrectPass()
        {
            if (OnCorrectPass != null)
                OnCorrectPass?.Invoke();
        }

        public static void InvokeIncorrectPass()
        {
            if (OnIncorrectPass != null)
                OnIncorrectPass?.Invoke();
        }

        public static void InvokeRetryRace()
        {
            if (OnRetryRace != null)
                OnRetryRace?.Invoke();
        }

        public static void InvokeNextLevel()
        {
            if (OnNextLevel != null)
                OnNextLevel?.Invoke();
        }

        public static void InvokeQuitGame()
        {
            if (OnQuitGame != null)
                OnQuitGame?.Invoke();
        }

        public static void InvokeQuitGameCancel()
        {
            if (OnQuitGameCancel != null)
                OnQuitGameCancel?.Invoke();
        }

        public static void InvokeRaceTimerStopped()
        {
            if (OnRaceTimerStopped != null)
                OnRaceTimerStopped?.Invoke();
        }
    }
}
