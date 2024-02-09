using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Used to get the vector 2 from the input data
    float keyData;

    // Used to add force to the players rigidbody
    Vector3 movementData;

    // Used to determine the force the player will jump with
    [SerializeField] float jumpForce;

    // Used to manage how fast the player will move
    [SerializeField] int speed;

    // Used to change the drag based on the players airborne status
    [SerializeField] int airDrag;
    [SerializeField] int groundDrag;

    // Used to change the speed of the player based on airborne status
    [SerializeField] int airSpeed;
    [SerializeField] int groundSpeed;

    // So we need both of these because some
    // Used to check if the player is touching the ground
    bool playerGrounded;

    // Used to check if the player is airborne 
    bool playerAirborne;

    // Used to calculate how long the position adjusting raycast should be
    float rayLength;

    // Used to create my own gravity
    [SerializeField] float gravity;

    // The players physics based "body"
    Rigidbody playerRB;

    // A mask for the layer that is ground
    [SerializeField] LayerMask ground;

    // Used to get the position of where the raycast hits the ground
    RaycastHit groundHit;

    // Used to tell if the player is holding the jump button
    bool isJumping;

    // Used time how long the player has held the jump button for
    float jumpHoldTimer;

    // Used to tell if the player is currently crouched
    bool isCrouched;

    // The players collider, needed for adjusting its size when crouching
    CapsuleCollider playerCollider;

    // The animator that manages the players animations
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns the players rigidbody
        playerRB = GetComponent<Rigidbody>();

        // Sets the player to defualt state of grounded
        playerGrounded = false;

        // Sets the player to the defualt state of airborne
        playerAirborne = true;

        // Defaults is jumping to false
        isJumping = false;

        // Gets the capsule collider
        playerCollider = GetComponent<CapsuleCollider>();

        // Calculates the ray length based on the players height and the offset
        rayLength = playerCollider.height / 2 + 0.1f;

        // Gets the animator
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // FixedUpdate is called dynamically for physics based operations
    void FixedUpdate()
    {
        if (!isCrouched)
        {
            // Applies force to players rigidbody based on the input
            playerRB.AddForce(movementData * speed);

            if (movementData.x != 0)
            {
                playerAnimator.SetBool("Running", true);
            }
            else
            {
                playerAnimator.SetBool("Running", false);
            }
        }

        // Shoots a raycasted line down from the player that detects ground
        playerGrounded = Physics.Raycast(playerRB.position, Vector3.down, out groundHit, rayLength, ground);

        //Debug.DrawRay(playerRB.position, new Vector3(0, -rayLength, 0), Color.red, 0.1f);

        // This checks if the players position is too high or too low based on the raycast and adjusts correctly but only plays when grounded as to not mess with jumping
        // This exists to allow the player to walk up slopes
        if (playerGrounded == true && (Vector3.Distance(playerRB.position, groundHit.point) < rayLength || Vector3.Distance(playerRB.position, groundHit.point) > rayLength))
        {
            playerRB.transform.SetPositionAndRotation(new Vector3(playerRB.position.x, groundHit.point.y + rayLength, playerRB.position.z), playerRB.transform.rotation);
        }

        // My own gravity system which I am only using because gravity is turned off for the player so that I don't have to stop their fall all the time in fixed update
        if (playerGrounded == false)
        {
            playerRB.AddForce(Vector3.down * gravity, ForceMode.Force);
        }
        else if (playerGrounded == true && playerAirborne == true) // We need both playerGrounded and playerAirborne here because this should only run the frame the player lands
        {
            // Zeros out the players up and down movement on being grounded so they do not actually touch the ground
            playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

            // Tells the code the player is no longer airborne
            playerAirborne = false;
        }
        
        // This basically checks if the player is still holding the jump button during a short time after pressing it to give them a larger jump than just tapping it
        if (isJumping && jumpHoldTimer > 0)
        {
            // Applies upward force
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Force);

            // Reduces timer
            jumpHoldTimer = jumpHoldTimer - Time.deltaTime;
        }
    }

    private void Update()
    {
        if (playerGrounded == true)
        {
            // Sets the player to airborne and adjusts speed and drag
            playerAirborne = false;
            playerRB.drag = groundDrag;
            speed = groundSpeed;
        }
        else if (playerGrounded == false)
        {
            // Sets the player to airborne and adjusts speed and drag
            playerAirborne = true;
            playerRB.drag = airDrag;
            speed = airSpeed;
        }
    }

    void OnMovement(InputValue keyInput)
    {
        // Gets the vector 2 from the stick input
        keyData = keyInput.Get<float>();

        // Puts the vector 2 into a vector 3 for applying force
        movementData = new Vector3(keyData, 0f, 0f);
    }

    // Is called on both press and release
    void OnJump()
    {
        // Reverses the state of jumping
        isJumping = !isJumping;

        // Checks if this call of the function set jumping to true and if the player is currently grounded
        if (isJumping && playerGrounded)
        {
            // Zeros out the players up and down movement for a smooth jump
            playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

            // Adds upward force to the player with impulse, so only once application
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Sets the hold timer which is used in fixed update above
            jumpHoldTimer = 0.5f;
        }
    }

    void OnCrouch()
    {
        // Reverses the crouched state
        isCrouched = !isCrouched;

        // Adjusts the size of the players collider based on the state
        if (isCrouched)
        {
            playerCollider.height = 1.5f;
            playerCollider.center = new Vector3(0, -0.5f, 0);
        }
        else if (!isCrouched)
        {
            playerCollider.height = 3;
            playerCollider.center = Vector3.zero;
        }
    }
}
