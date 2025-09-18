using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerEvents 
    {
        /// <summary>
        /// Represents a method that will handle an event when a player is hit.
        /// </summary>
        /// <remarks>This delegate can be used to define custom logic that executes when a player is hit
        /// in a game. It does not take any parameters and does not return a value.</remarks>
        public delegate void PlayerHitAction(GameObject hitObject);
        public static event PlayerHitAction OnPlayerHit;
        
        /// <summary>
        /// Invokes the <see cref="OnPlayerHit"/> event to signal that a player has been hit.
        /// </summary>
        /// <remarks>This method triggers the <see cref="OnPlayerHit"/> event, allowing subscribers to
        /// respond to the player being hit. Ensure that there are no null subscribers to avoid potential <see
        /// cref="NullReferenceException"/>.</remarks>
        public static void PlayerHit(GameObject hitObject)
        {
            OnPlayerHit?.Invoke(hitObject);
        }
    }
}
