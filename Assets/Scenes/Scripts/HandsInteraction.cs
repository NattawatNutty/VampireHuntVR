﻿// Nattawat Puakpaiboon @Universität Bremen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandsEvents : MonoBehaviour
{

    //// Variable part ////
    private EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;        // Trigger on VR controller
    private EVRButtonId touchpad = EVRButtonId.k_EButton_SteamVR_Touchpad;      // Touchpad on VR controller
    public Hand leftHand;                                                       // Left-handed controller
    public Hand rightHand;                                                      // Right-handed controller

    // Initialization
    private void Start()
    {
        
    }

    // For handling the triggers events (painting/drawing in the air like in Tilt Brush) by LineRenderer
    private void FixedUpdate()
    {
        if (leftHand == null || !leftHand.isActiveAndEnabled)
        {
            Debug.Log("Left controller has not been not initialized.");
            return;
        }

        if (rightHand == null || !rightHand.isActiveAndEnabled)
        {
            Debug.Log("Right controller has not been not initialized.");
            return;
        }

        //////////////////////////////////////////////////////////
        //// Before Drawing (color & thickness pre-selection) ////
        //////////////////////////////////////////////////////////

        /*
        // Select the thickness before drawing
        if (leftHand.controller.GetTouchUp(touchpad) && !rightHand.controller.GetPress(trigger))
        {

        }

        // Select the new color before drawing
        if (rightHand.controller.GetTouchDown(touchpad) && !rightHand.controller.GetPress(trigger))
        {

        }

        ////////////////////////
        //// Group Checking ////
        ////////////////////////

        // Check whether a new line is near any existing groups of lines right at the moment the trigger is pressed
        if (rightHand.controller.GetPressDown(trigger))
        {
            
        }

        /////////////////
        //// Drawing ////
        /////////////////

        // Hold the right trigger to draw in the air
        if (rightHand.controller.GetPress(trigger))
        {
            
        }

        // After releasing the right trigger, the list of points drawing a line will be eliminated for drawing the next line
        if (rightHand.controller.GetPressUp(trigger))
        {
            
        }
        */
    }
}