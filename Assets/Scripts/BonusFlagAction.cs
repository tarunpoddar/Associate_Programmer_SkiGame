using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an action triggered when a player passes a bonus flag in the game.
    /// </summary>
    /// <remarks>This class listens for collision events with objects tagged as "Player" and triggers a bonus
    /// event  when the player passes through the flag for the first time. Subsequent collisions are ignored.</remarks>
    public class BonusFlagAction : MonoBehaviour
    {
        private bool flagPassed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !flagPassed)
            {
                flagPassed = true;
                GameEvents.InvokeBonusPass();
            }
        }
    }
}
