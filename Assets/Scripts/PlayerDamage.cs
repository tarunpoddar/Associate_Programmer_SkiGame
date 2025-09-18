using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Handles player damage mechanics, including knockback, recovery, and health updates,  when the player collides
    /// with obstacles.
    /// </summary>
    /// <remarks>This class listens for player hit events and applies knockback forces, updates the player's
    /// health,  and manages recovery time before the player can resume normal movement. It interacts with the  player's
    /// Rigidbody and PlayerController components.</remarks>
    public class PlayerDamage : MonoBehaviour
    {
        [Tooltip("How much force knocks the player backwards after crashing into an obstacle.")]
        public float knockBackForce;
        [Tooltip("How much force knocks the player backwards after crashing into an obstacle.")]
        public float knockUpForce;
        [Tooltip("How many seconds before the player can move downhill again after crashing into an obstacle.")]
        public float recoveryTime;
        [Tooltip("Checks when the player is hurt.")]
        public bool hurt = false;

        private Rigidbody rb;
        private PlayerController playerController;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerController = GetComponent<PlayerController>();
        }

        // Register that TakeDamage will be called when an OnPlayerHit Event happens
        private void OnEnable()
        {
            PlayerEvents.OnPlayerHit += TakeDamage;
        }

        // Un Register TakeDamade will be called when an OnPlayerHit Event happens
        private void OnDisable()
        {
            PlayerEvents.OnPlayerHit -= TakeDamage;
        }

        private void TakeDamage(GameObject hitObject)
        {
            if (!hurt)
            {
                hurt = true;
                rb.linearVelocity = Vector3.zero;

                // sends the player up and back from bumping into an obstacle
                rb.AddForce(transform.forward * -knockBackForce); // Going up.
                rb.AddForce(transform.up * knockUpForce);

                StartCoroutine("Recover");
            }
        }

        IEnumerator Recover()
        {
            yield return new WaitForSeconds(recoveryTime);
            hurt = false;
        }
    }
}
