using UnityEngine;

namespace SkiGame
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
            public int health;
        }

        [Tooltip("Keyboard controls for steering left")]
        public KeyCode left = KeyCode.A;
        
        [Tooltip("Keyboard controls for steering right.")]
        public KeyCode right = KeyCode.D;

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

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Debug.Log($"Euler Angles: {transform.rotation.eulerAngles:F0}");

            if (isMoving)
            {
                bool isGrounded = Physics.Linecast(transform.position, groundCheck.position, groundLayers);

                if (isGrounded)
                {
                    if (Input.GetKey(left))
                    {
                        TurnLeft();
                    }

                    if (Input.GetKey(right))
                    {
                        TurnRight();
                    }
                }
            }
        }

        /// <summary>
        /// Processing Physics and Movement.
        /// </summary>
        private void FixedUpdate()
        {
            ControlSpeed();

            if (isMoving)
            {
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
        private float Remap(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
        {
            float OldRange = (OldMax - OldMin);
            float NewRange = (NewMax - NewMin);
            float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
            return (NewValue);
        }
    }
}
