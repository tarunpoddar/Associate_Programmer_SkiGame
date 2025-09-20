using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an action that triggers the start of a race when the player interacts with the starting flag.
    /// </summary>
    /// <remarks>This component should be attached to the starting flag object in the scene. It listens for
    /// the player entering the trigger zone and starts the race if the player passes the flag correctly. If the player
    /// bypasses the flag incorrectly, an event is triggered to handle the incorrect pass.</remarks>
    public class StartFlagAction : MonoBehaviour
    {
        bool correctPass = false;
        private GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                correctPass = true;
                GameEvents.StartRace();
            }
        }

        private void Update()
        {
            if (!correctPass && player.transform.position.z < transform.position.z)
            {
                // Perform actions for incorrect pass
                GameEvents.IncorrectPass();
                correctPass = true; // Prevent multiple triggers
            }
        }
    }
}
