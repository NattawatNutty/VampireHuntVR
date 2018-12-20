using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    // Attach this script to right hand VR controller

    // Variable part //
    private SteamVR_Action_Vector2 touchpadCoor;                                // Touchpad action
    private Vector3 movement;                                                   // Movement of the player according to the touch pad position

    public GameObject player;
    public GameObject playerCam;
    public float turnRate = 0.2f;
    public Hand hand;                                                           // The hand this script attached to
    public float speed;                                                         // Speed of the movement range [0.0, 1.0]

    // Call when the script is active
    private void Start()
    {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                                   // Get the Hand component
        }
    }

    private void FixedUpdate()
    {
        // Touchpad input from the player
        touchpadCoor = SteamVR_Input._default.inActions.TouchPos;
        // Touch pad position
        Vector2 touchPos = touchpadCoor.GetAxis(hand.handType);
        Vector2 touchPosRescaled = new Vector2(touchPos.x / 20f, touchPos.y / 20f);
        Debug.Log(touchPosRescaled);

        //if(touchPosRescaled.x)
        movement = new Vector3(touchPosRescaled.x, 0, touchPosRescaled.y);      // Pass the vector to the moveoment direction

        // Get the rotation of the player (which direction the player is looking)
        //Quaternion rotationCam = playerCam.transform.rotation;
        //Vector3 targetDir = player.transform.TransformDirection(movement);
        //player.transform.rotation = Quaternion.FromToRotation(player.transform.forward, playerCam.transform.forward);
        player.transform.rotation = Quaternion.LookRotation(player.transform.forward);

        player.transform.position += movement;                                  // Change the position of the player according to the touch pad position
    }
}
