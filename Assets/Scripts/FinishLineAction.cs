using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an action triggered when a player crosses the finish line.
    /// </summary>
    /// <remarks>This class listens for collisions with the finish line and triggers the end-of-race event
    /// when a player crosses it. It is intended to be attached to a GameObject representing the finish line in a Unity
    /// scene.</remarks>
    public class FinishLineAction : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameEvents.EndRace();
                Debug.Log("Race Finished!");
            }
        }
    }
}
