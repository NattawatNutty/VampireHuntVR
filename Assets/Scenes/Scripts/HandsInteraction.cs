﻿// Nattawat Puakpaiboon @Universität Bremen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandsInteraction : MonoBehaviour
{
    //// Variable part ////
    private Hand hand;                                                          // The hand this script attached to

    // Initialization
    private void Start()
    {
        hand = GetComponent<Hand>();                                            // Get the Hand component
    }

    // For handling the triggers events (painting/drawing in the air like in Tilt Brush) by LineRenderer
    private void FixedUpdate()
    {
        if (hand == null)
        {
            Debug.Log("The controller is not loaded.");
        }

        // If left hand
        if (hand.handType == SteamVR_Input_Sources.LeftHand)
        {
            // Character movement
        }
        // If right hand
        else if (hand.handType == SteamVR_Input_Sources.RightHand)
        {
            // Select an object with HOMER technique

            // Hold the object
        }
    }
}