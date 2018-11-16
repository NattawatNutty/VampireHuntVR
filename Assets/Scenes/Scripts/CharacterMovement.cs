using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    //// Variable part ////
    private SteamVR_Action_Vector2 touchpadCoor;                // Touchpad action
    private Vector3 movement;                                   // Movement of the player according to the touch pad position
    private bool jump;                                          // Is the player jump?
    public GameObject player;
    public Hand hand;                                           // The hand this script attached to
    public float speed;                                         // Speed of the movement range [0.0, 1.0]
    [SteamVR_DefaultAction("Jump", "platformer")]
    public SteamVR_Action_Boolean a_jump;

    // Call when the script is active
    private void Start()
    {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                   // Get the Hand component
        }

        if (touchpadCoor == null)
        {
            Debug.LogError("No touch pad coordinate.");
            return;
        }

    }

    private void playerMove()
    {
        // Touchpad input from the player
        SteamVR_Action_Vector2 touchpadCoor = SteamVR_Input._default.inActions.TouchPos;
        // Touch pad position
        Vector2 touchPos = touchpadCoor.GetAxis(hand.handType);
        movement = new Vector3(touchPos.x, 0, touchPos.y);      // Pass the vector to the moveoment direction
        jump = a_jump.GetStateDown(hand.handType);              // Jump when the player press the touchpad

        movement = transform.InverseTransformDirection(movement);
        player.transform.position += movement;
    }
}
