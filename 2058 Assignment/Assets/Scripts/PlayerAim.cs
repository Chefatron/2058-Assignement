using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    // The screen coordinates of the mouse
    Vector2 mousePosition;

    // The object that we will physically move around to tell the game where the player is aiming
    [SerializeField] Transform aimObject;

    // The position that the raycast drawn by the mouse position into physical space hit
    RaycastHit aimHit;

    // The layer that the raycast should register a hit on
    [SerializeField] LayerMask aimLayer;

    // The players rigidbody
    Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        // Shoots the raycast to translate the players mouse position to a postion in physical world space
        Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out aimHit, Mathf.Infinity, aimLayer);

        // Sets the aim object to that position
        aimObject.SetPositionAndRotation(aimHit.point, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        playerRB.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(aimObject.transform.position.x - playerRB.position.x), 0)));
    }

    void OnAim(InputValue mouseInput)
    {
        // Gets the vector 2 from the mouse pos
        mousePosition = mouseInput.Get<Vector2>();

        //Debug.Log("-----------------------------------------------");
        //Debug.Log("-----------------------------------------------");
        //Debug.Log("-----------------------------------------------");
        //Debug.Log("This is the x value: " + mousePosition.x);
        //Debug.Log("-----------------------------------------------");
        //Debug.Log("This is the y value: " + mousePosition.y);
    }
}
