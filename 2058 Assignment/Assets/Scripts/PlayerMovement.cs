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

    // Used to get the players height for accurate raycasting
    float height;

    // Used to create my own gravity
    [SerializeField] float gravity;

    // The players physics based "body"
    Rigidbody playerRB;

    // A mask for the layer that is ground
    [SerializeField] LayerMask ground;

    RaycastHit groundHit;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns the players rigidbody
        playerRB = GetComponent<Rigidbody>();

        // Sets the player to defualt state of grounded
        playerGrounded = false;

        // Sets the player to the defualt state of airborne
        playerAirborne = true;

        // Gets the height of the player
        height = GetComponent<CapsuleCollider>().height;
    }

    // FixedUpdate is called dynamically for physics based operations
    void FixedUpdate()
    {
        // Applies force to players rigidbody based on the input
        playerRB.AddRelativeForce(movementData * speed);

        // Shoots a raycasted line down from the player that detects ground
        playerGrounded = Physics.Raycast(playerRB.position, Vector3.down, out groundHit, height / 2 + 0.5f, ground);

        // This checks if the players position is too high or too low based on the raycast and adjusts correctly but only plays when grounded as to not mess with jumping
        // This exists to allow the player to walk up slopes
        if (playerGrounded == true && (Vector3.Distance(playerRB.position, groundHit.point) < height / 2 + 0.1f || Vector3.Distance(playerRB.position, groundHit.point) > height / 2 + 0.1f))
        {
            playerRB.transform.SetPositionAndRotation(new Vector3(playerRB.position.x, groundHit.point.y + height / 2 + 0.1f, playerRB.position.z), playerRB.transform.rotation);
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
        movementData = new Vector3(keyData, 0.0f, 0.0f);
    }

    void OnJump()
    {
        if (playerGrounded == true)
        {
            // Zeros out the players up and down movement for a smooth jump
            playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

            // Adds upward force to the player with impulse, so only once application
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
