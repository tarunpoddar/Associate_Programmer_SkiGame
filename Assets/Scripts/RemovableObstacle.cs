using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents an obstacle that can be removed from the game when interacted with by the player.
    /// </summary>
    /// <remarks>This class extends the <see cref="Obstacle"/> base class and overrides its behavior to
    /// include destruction of the obstacle upon interaction with the player.</remarks>
    public class RemovableObstacle : Obstacle
    {
        public override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HitPlayer(collision.gameObject, gameObject);
            }

            if (!collision.gameObject.CompareTag("Slope"))
            {
                Destroy(gameObject);
            }
        }
    }
}
