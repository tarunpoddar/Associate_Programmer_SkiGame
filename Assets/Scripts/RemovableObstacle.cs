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
        /// <summary>
        /// Applies a hit to the player and destroys the current game object.
        /// </summary>
        /// <remarks>This method overrides the base implementation of <see cref="HitPlayer"/>.  After
        /// invoking the base method, it destroys the game object associated with this instance.</remarks>
        public override void HitPlayer(GameObject player, GameObject hitObject)
        {
            base.HitPlayer(player, hitObject);

            Destroy(gameObject);
        }
    }
}
