using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandsInteraction : MonoBehaviour {
    // Variable
    private EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
    private EVRButtonId touchpad = EVRButtonId.k_EButton_SteamVR_Touchpad;
    public Hand leftHand;
    public Hand rightHand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
    }
}
