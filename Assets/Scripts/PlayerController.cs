using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    /// <summary>
    /// Manages player input and controls within the game.
    /// </summary>
    /// <remarks>This class is responsible for handling player interactions and updating the player's state   
    /// during each frame. Attach this script to a GameObject to enable player control functionality.</remarks>
    public class PlayerController : MonoBehaviour
    {
        [System.Serializable]
        public struct Stats
        {
            [Tooltip("Speed of the player.")]
            public float speed;

            [Tooltip("Maximum speed of the player.")]
            public float maxSpeed;

            [Tooltip("Minimum speed of the player.")]
            public float minSpeed;

            [Tooltip("Turn speed of the player.")]
            public float turnSpeed;

            [Tooltip("Turn acceleration of the player.")]
            public float turnAcceleration;

            [Tooltip("Turn Deacceleration of the player.")]
            public float turnDeceleration;

            public int score;

            public float health;
        }

        [Tooltip("Is the player moving.")]
        public bool isMoving;

        [Tooltip("Assign the ground check game object from the player.")]
        public Transform groundCheck;

        [Tooltip("Assign the ground layers from the unity.")]
        public LayerMask groundLayers;

        [Tooltip("Current player stats.")]
        public Stats playerStats;

        private Rigidbody rb;
        private Animator animator;
        private PlayerDamage playerDamage;
        private float movementX;
        private bool raceStopped = false;
        private bool isBoosting = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            playerDamage = GetComponent<PlayerDamage>();
        }

        private void OnEnable()
        {
            GameEvents.OnRaceOver += OnRaceStopped;
        }

        private void OnRaceStopped()
        {
            raceStopped = true;
        }

        public void UpdateHealth(float amount)
        {
            playerStats.health += amount;
            Debug.Log($"Updated health by: {amount}, Player's current Health: {playerStats.health}");
        }

        public IEnumerator BoostSpeed()
        {
            isBoosting = true;
            playerStats.speed += 50f;
            yield return new WaitForSeconds(2);
            playerStats.speed -= 50f;
            isBoosting = false;
        }

        /// <summary>
        /// Processing Physics and Movement.
        /// </summary>
        private void FixedUpdate()
        {
            if (raceStopped) return; // Disable movement when race is stopped.

            if (!isBoosting)
            {
                ControlSpeed();
            }

            bool isGrounded = Physics.Linecast(transform.position, groundCheck.position, groundLayers);

            if (isMoving && !playerDamage.hurt)
            {
                if (isGrounded)
                {
                    const float tolerance = 0.01f; // Define a small tolerance for floating-point comparison

                    if (Mathf.Abs(movementX - 1) < tolerance)
                    {
                        TurnRight();
                    }
                    else if (Mathf.Abs(movementX + 1) < tolerance)
                    {
                        TurnLeft();
                    }
                }

                // increase or decrease the players speed depending on how much they are facing downhill
                float turnAngle = Mathf.Abs(180 - transform.eulerAngles.y);
                playerStats.speed += Remap(0, 90, playerStats.turnAcceleration, -playerStats.turnDeceleration, turnAngle);

                // moves the player forward based on which direction they are facing
                Vector3 velocity = (transform.forward) * playerStats.speed * Time.fixedDeltaTime;
                velocity.y = rb.linearVelocity.y;
                rb.linearVelocity = velocity;
            }

            // update the Animator's state depending on our speed
            animator.SetFloat("playerSpeed", playerStats.speed);
        }

        /// <summary>
        /// Uses unity's new input system to get the player's movement input.
        /// </summary>
        /// <param name="movementValue"></param>
        private void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;// Horizontal movement , 1= right, -1 = left
        }

        private void TurnLeft()
        {
            // rotates the player, limiting them after reaching a certain angle
            if (transform.eulerAngles.y > 91)
            {
                transform.Rotate(new Vector3(0, -playerStats.turnSpeed, 0) * Time.deltaTime, Space.Self);
            }
        }

        private void TurnRight()
        {
            if (transform.eulerAngles.y < 269)
            {
                transform.Rotate(new Vector3(0, playerStats.turnSpeed, 0) * Time.deltaTime, Space.Self);
            }
        }

        private void ControlSpeed()
        {
            // limits the player's speed when reaching past the speed maximum
            if (playerStats.speed > playerStats.maxSpeed)
            {
                playerStats.speed = playerStats.maxSpeed;
            }

            // limits the player from moving any slower than the speed minimum
            if (playerStats.speed < playerStats.minSpeed)
            {
                playerStats.speed = playerStats.minSpeed;
            }
        }

        // remaps a number from a given range into a new range
        private static float Remap(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
        {
            float OldRange = (OldMax - OldMin);
            float NewRange = (NewMax - NewMin);
            float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
            return (NewValue);
        }
    }
}
