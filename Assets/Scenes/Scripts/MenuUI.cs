using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MenuUI : MonoBehaviour {

    // Attach this script to right hand VR controller

    // Variable part //
    private float rayWidth = 0.05f;                                             // The width of the ray
    public float grabRange = 50f;                                               // The range of the ray
    public Hand hand;                                                           // The hand this script attached to
    public Canvas mainMenuUI;

    // Use this for initialization
    void Start () {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                                   // Get the Hand component
        }
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hitInfo;

        bool hit = Physics.Raycast(hand.transform.position, hand.transform.forward, out hitInfo, grabRange);

        if (hit) {
            if(hitInfo.collider.gameObject.name == "Start") {
                GameObject startButton = hitInfo.collider.gameObject;
            }
            else if (hitInfo.collider.gameObject.name == "Option") {

            }
            else if (hitInfo.collider.gameObject.name == "Exit") {

            }
        }
    }
}
