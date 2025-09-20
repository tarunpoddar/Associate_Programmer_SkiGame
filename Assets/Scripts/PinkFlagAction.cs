using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an action triggered when a player interacts with a pink flag in the game.
    /// </summary>
    /// <remarks>This class defines behavior for determining whether a player passes or fails based on their
    /// position relative to the flag when entering its trigger area. The specific actions for passing or failing are
    /// defined in the base <see cref="FlagAction"/> class.</remarks>
    public class PinkFlagAction : FlagAction
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.gameObject.transform.position.x < transform.position.x)
                {
                    PassAction();
                }
                else
                {
                    FailAction();
                }
            }
        }
    }
}
