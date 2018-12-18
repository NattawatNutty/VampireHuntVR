using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

// The required scripts for menu UI
[RequireComponent(typeof(SelectWeapon))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(Shooter))]

public class MenuUI : MonoBehaviour {

    // Attach this script to right hand VR controller

    // Variable part //
    private float rayWidth = 0.01f;                                             // The width of the ray
    public LineRenderer raySelect;                                              // LineRenderer for casting the ray
    public float grabRange = 50f;                                               // The range of the ray
    public Hand hand;                                                           // The hand this script attached to
    public bool mode;

    public GameObject hitObject;                                                // The game object that the raycast from the controller hit
    public GameObject button;                                                   // The button that the raycast hit
    public GameObject mainMenu;                                                 // Main menu UI canvas
    public GameObject startMenu;                                                // Start menu UI canvas
    public GameObject optionMenu;                                               // Option menu UI canvas

    private string homerTech = "HOMER Technique";
    private string simRayCastTech = "Simple Raycast Technique";
    public Text techniqueText;                                                  // Text showing current selection technique (default: HOMER)

    // Use this for initialization
    void Start () {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                                   // Get the Hand component
        }

        // Disable the scripts involving with the gameplay until the player begins to play
        hand.GetComponent<SelectWeapon>().enabled = false;
        hand.GetComponent<CharacterMovement>().enabled = false;
        hand.GetComponent<Shooter>().enabled = false;

        techniqueText.text = homerTech;
        raySelect.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        mode = transform.root.gameObject.GetComponent<PlayerUI>().gameplayMode;

        RayCastHandler();

        // Select 'Start' menu
        if (getInteractUIDown() && button.name == "Start") {
            startMenu.SetActive(true);
            mainMenu.SetActive(false);
            StartMenu();
        }
        // Select 'Option' menu
        else if (getInteractUIDown() && button.name == "Option") {
            OptionMenu();
        }
        // Select 'Exit'
        else if (getInteractUIDown() && button.name == "Exit") {
            Application.Quit();
        }  
    }

    public void StartMenu() {
        RayCastHandler();

        // Proceed to play tutorial
        if (getInteractUIDown() && button.name == "Play tutorial") {
            startMenu.SetActive(false);

            // Enable the scripts involving with the gameplay
            hand.GetComponent<SelectWeapon>().enabled = true;
            hand.GetComponent<CharacterMovement>().enabled = true;
            hand.GetComponent<Shooter>().enabled = true;
        }
        // Proceed to the village scene
        else if (getInteractUIDown() && button.name == "Skip") {
            startMenu.SetActive(false);
        }
    }

    public void OptionMenu() {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);

        RayCastHandler();
    }

    // Handle raycast events on the button of the menu
    public void RayCastHandler() {
        RaycastHit hitInfo;                                                     // Information of the object hit by the raycast
        
        // Player press the trigger to select UI
        bool hit = Physics.Raycast(hand.transform.position, hand.transform.forward, out hitInfo, grabRange);

        // If the raycast hits something
        if (hit) {
            // Get the game object that hit by the raycast
            hitObject = hitInfo.collider.gameObject;
            // If hit object is the button named Cube i.e. button
            if (hitObject.name == "Cube") {
                button = hitObject.transform.parent.gameObject;                 // Pass button object reference
                float lineDistance = Vector3.Distance(transform.position, hitObject.transform.position);

                // Set the ray casting from the controller to the weapon
                raySelect.enabled = true;
                raySelect.startWidth = rayWidth;
                raySelect.endWidth = rayWidth;
                raySelect.SetPosition(0, transform.position);
                raySelect.SetPosition(1, transform.position + transform.forward * lineDistance);
            } else {
                raySelect.enabled = false;
                button = null;
                hitObject = null;
            }
        } else {
            raySelect.enabled = false;
            button = null;
            hitObject = null;
        }
    }

    public bool getInteractUIDown() {
        return SteamVR_Input._default.inActions.InteractUI.GetStateDown(hand.handType);
    }
}
