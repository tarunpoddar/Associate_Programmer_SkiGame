using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an obstacle in the game that interacts with the player upon collision.
    /// </summary>
    /// <remarks>This class is designed to handle collisions with the player and trigger appropriate
    /// responses,  such as reducing player health or playing a sound. It can be extended to define custom behavior  by
    /// overriding the <see cref="HitPlayer"/> method.</remarks>
    public class Obstacle: MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HitPlayer(collision.gameObject, gameObject);
            }
        }

        /// <summary>
        /// Handle the collision event, e.g., reduce player health, play sound, etc.
        /// </summary>
        public virtual void HitPlayer(GameObject player, GameObject hitObject)
        {
            PlayerEvents.PlayerHit(hitObject);
            player.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }
}
