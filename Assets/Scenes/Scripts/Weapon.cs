using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

// The required scripts for weapon
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(VelocityEstimator))]

public class Weapon : MonoBehaviour
{
    //// Variable part ////
    private bool isAttached = false;            // Is this weapon attached to any hand?
    private Vector3 attachPosition;             // The attached position
    private Quaternion attachRotation;          // The attached rotation

    // Use this for initialization
    void Start () {
        tag = "Weapon";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
