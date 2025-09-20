using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an action that determines whether the player has correctly passed a specific point in the game.
    /// </summary>
    /// <remarks>This class monitors the player's position relative to a trigger zone and raises events to
    /// indicate whether the pass was correct or incorrect. A pass is considered correct if the player enters the
    /// trigger zone. If the player moves past the trigger zone without entering it, the pass is considered
    /// incorrect.</remarks>
    public class PassFlagAction : MonoBehaviour
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
                GameEvents.CorrectPass();
                Debug.Log("Correct Pass !!");
            }
        }

        private void Update()
        {
            if (!correctPass && player.transform.position.z < transform.position.z)
            {
                // Perform actions for incorrect pass
                GameEvents.IncorrectPass();
                Debug.Log("Incorrect Pass!");
                correctPass = true; // Prevent multiple triggers
            }
        }
    }
}
