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

        public delegate void RaceOverAction();
        public static event RaceOverAction OnRaceOver;

        public delegate void CorrectPassAction();
        public static event CorrectPassAction OnCorrectPass;

        public delegate void IncorrectPassAction();
        public static event IncorrectPassAction OnIncorrectPass;

        public static void StartRace()
        {
            OnRaceStart?.Invoke();
        }

        public static void EndRace()
        {
            OnRaceOver?.Invoke();
        }

        public static void CorrectPass()
        {
            OnCorrectPass?.Invoke();
        }

        public static void IncorrectPass()
        {
            OnIncorrectPass?.Invoke();
        }
    }
}
