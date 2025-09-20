using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Increase the player's speed when they pass the flag from the correct direction.
    /// </summary>
    public class BoostFlagAction : FlagAction
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PassAction();
                var playerController = other.GetComponent<PlayerController>();
                
                if (playerController != null)
                {
                    StartCoroutine(playerController.BoostSpeed());
                }
            }
        }
    }
}
