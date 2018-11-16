using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

// The required scripts for weapon
[RequireComponent(typeof(Rigidbody))]

public class Weapon : MonoBehaviour
{
    //// Variable part ////
    public bool isAttached = false;             // Is this weapon attached to any hand?

    // Use this for initialization
    void Start () {
        tag = "Weapon";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
