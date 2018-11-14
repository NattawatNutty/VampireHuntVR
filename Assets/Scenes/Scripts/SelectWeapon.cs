﻿// Nattawat Puakpaiboon @Universität Bremen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SelectWeapon : MonoBehaviour
{
    //// Variable part ////
    private SteamVR_Action_Boolean grabAction;                  // Grab action
    private Hand hand;                                          // The hand this script attached to
    public GameObject weapon;                                   // The weapon that the ray hit

    // Call when the script is active
    private void OnEnable()
    {
        if (hand == null)
        {
            hand = GetComponent<Hand>();                         // Get the Hand component
        }

        if (grabAction == null)
        {
            Debug.LogError("No grab action assigned");
            return;
        }

        grabAction.AddOnChangeListener(OnGrabActionChange, hand.handType);
    }

    // Call when the script is inactive
    private void OnDisable()
    {
        if (grabAction == null)
        {
            if (grabAction != null)
                grabAction.RemoveOnChangeListener(OnGrabActionChange, hand.handType);
        }
    }

    // When the user push the trigger
    private void OnGrabActionChange(SteamVR_Action_In actionIn)
    {
        if (grabAction.GetStateDown(hand.handType))
        {
            Grab();
        }
    }

    // Start doing grab action
    public void Grab()
    {
        StartCoroutine(DoGrab());
    }

    // Perform grabbing action
    private IEnumerator DoGrab()
    {
        RaycastHit hitInfo;                                 // Information from the raycast hitting that weapon
        bool hit = Physics.Raycast(hand.transform.position, Vector3.forward, out hitInfo);
        // If the raycast hit the weapon
        if (hit)
        {
            weapon = hitInfo.collider.gameObject;           // Pass the detected game object to the  weapon variable
        }
        else
        {
            weapon = null;
        }

        yield return null;
    }
}