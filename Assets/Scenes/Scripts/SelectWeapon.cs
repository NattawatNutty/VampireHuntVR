// ITCS496
// Team members
// Nattawat         Puakpaiboon 5888158

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SelectWeapon : MonoBehaviour
{
    //// Variable part ////
    private SteamVR_Action_Boolean grabAction;                  // Grab action
    private bool isAttached = false;                            // Is a weapon attach to the hand?
    public float grabRange = 50f;                               // The range of the ray
    public Hand hand;                                           // The hand this script attached to
    public GameObject weapon;                                   // The weapon that the ray hit

    // Call when the script is active
    private void Start()
    {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                   // Get the Hand component
        }
    }

    private void Update()
    {
        RaycastHit hitInfo;                                     // Information of the object hit by the raycast

        if (isAttached)
        {
            return;
        }

        // Check whether the raycast from the right controller hits a collider
        bool hit = Physics.Raycast(hand.transform.position, hand.transform.forward, out hitInfo, grabRange);
        // If the raycast hits something
        if (hit)
        {
            //Debug.Log("Hit");
            //Debug.DrawRay(hand.transform.position, hand.transform.forward);

            // If hit the weapon
            if (hitInfo.collider.gameObject.tag == "Weapon")
            {
                Debug.Log("Hit the weapon");
                weapon = hitInfo.collider.gameObject;           // Obtain the weapon game object

                // If the player press the trigger, attach the weapon to the player's right hand
                if (getSelectWeapon())
                {

                }
            }
            else
            {
                weapon = null;                                  // Clear the weapon
            }
        }
    }

    public bool getSelectWeapon()
    {
        return SteamVR_Input._default.inActions.SelectWeapon.GetStateDown(hand.handType);
    }
}