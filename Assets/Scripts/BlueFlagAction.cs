using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an action triggered when a player interacts with the blue flag.
    /// </summary>
    /// <remarks>This class defines behavior for determining whether a player successfully passes or fails
    /// based on their position relative to the flag when entering its trigger area.</remarks>
    public class BlueFlagAction : FlagAction
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.gameObject.transform.position.x > transform.position.x)
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
