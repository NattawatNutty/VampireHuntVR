﻿// ITCS496
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
    //private SteamVR_Action_Boolean grabAction;                                  // Grab action
    public bool isAttached = false;                                             // Is a weapon attach to a hand?
    public float grabRange = 50f;                                               // The range of the ray
    public Hand hand;                                                           // The hand this script attached to
    public GameObject weapon;                                                   // The current selected weapon

    // Use this for initialization
    private void Start()
    {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                                   // Get the Hand component
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;                                                     // Information of the object hit by the raycast

        // Check whether the raycast from the right controller hits a collider
        bool hit = Physics.Raycast(hand.transform.position, hand.transform.forward, out hitInfo, grabRange);
        // If the raycast hits something
        if (hit)
        {
            //Debug.Log("Hit");
            Debug.DrawRay(hand.transform.position, hand.transform.forward);

            // If the raycast hit the weapon
            if (hitInfo.collider.gameObject.tag == "Weapon")
            {
                //Debug.Log("Hit the weapon");
                weapon = hitInfo.collider.gameObject;                           // Obtain the weapon game object

                // If the player press the trigger, attach the weapon to the player's right hand
                if (getSelectWeapon() && !isAttached)
                {
                    //Debug.Log("Weapon selected");
                    isAttached = true;
                    weapon.GetComponent<Rigidbody>().isKinematic = true;        // Ignore the gravity
                    weapon.transform.position = transform.position;
                    weapon.transform.rotation = transform.rotation;
                    weapon.transform.parent = transform;                        // Set parent of the weapon to the hand
                }
                // If the player press the grip while holding the weapon, detach the weapon
                else if (getReleaseWeapon() && isAttached)
                {
                    //Debug.Log("Weapon released");
                    isAttached = false;
                    weapon.GetComponent<Rigidbody>().isKinematic = false;
                    weapon.transform.parent = null;                             // Detach the object from the hand
                }
            }
            else
            {
                isAttached = false;
                weapon = null;                                                  // Clear the weapon
            }
        }
    }

    public bool getSelectWeapon()
    {
        return SteamVR_Input._default.inActions.SelectWeapon.GetState(hand.handType);
    }

    public bool getReleaseWeapon()
    {
        return SteamVR_Input._default.inActions.ReleaseWeapon.GetStateDown(hand.handType);
    }
}