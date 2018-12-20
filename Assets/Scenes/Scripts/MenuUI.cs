﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float lineDistance = 20f;                                           // Distance of the raycase
    public LineRenderer ray;                                                    // LineRenderer component
    public GameObject raySelect;                                                // LineRenderer for casting the ray
    public float grabRange = 50f;                                               // The range of the ray
    public Hand hand;                                                           // The hand this script attached to
    public bool gameplayMode;                                                   // Does the player enter the game mode?
    public bool isPause = false;                                                // Has the player entered pause menu?

    public GameObject hitObject;                                                // The game object that the raycast from the controller hit
    public GameObject button;                                                   // The button that the raycast hit
    public GameObject mainMenu;                                                 // Main menu UI canvas
    public GameObject startMenu;                                                // Start menu UI canvas
    public GameObject optionMenu;                                               // Option menu UI canvas
    public GameObject pauseMenu;                                                // Pause menu while playing the game
    public GameObject quitToEnterGameMenu;
    public GameObject playerUI;

    private string homerTech = "HOMER Technique";
    private string simRayCastTech = "Simple Raycast Technique";
    public Text techniqueText;                                                  // Text showing current selection technique (default: HOMER)

    // Use this for initialization
    void Start () {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                                   // Get the Hand component
        }

        // If the player is in game
        if (gameplayMode) {
            // Enable the scripts involving with the gameplay
            hand.GetComponent<SelectWeapon>().enabled = true;
            hand.GetComponent<CharacterMovement>().enabled = true;
            hand.GetComponent<Shooter>().enabled = true;

            playerUI.SetActive(true);
        }
        else if (!gameplayMode) {
            // Disable the scripts involving with the gameplay until the player begins to play
            hand.GetComponent<SelectWeapon>().enabled = false;
            hand.GetComponent<CharacterMovement>().enabled = false;
            hand.GetComponent<Shooter>().enabled = false;

            playerUI.SetActive(false);
        }

        techniqueText.text = homerTech;
        Time.timeScale = 0;
    }

    private void Update() {
        // Handle raycast laser for the menu UI
        RayCastHandler();

        // Pause handle
        if (GetPauseDown() && !isPause) {
            isPause = true;
            pauseMenu.SetActive(true);

            // Disable the scripts involving with the gameplay until the player resumes to play again
            hand.GetComponent<SelectWeapon>().enabled = false;
            hand.GetComponent<CharacterMovement>().enabled = false;
            hand.GetComponent<Shooter>().enabled = false;

            playerUI.SetActive(false);
            Time.timeScale = 0;
        } else if (GetPauseDown() && isPause) {
            ResumeGame();
        }

        if (button != null) {
            // Select 'Start' menu
            if (GetInteractUIDown() && button.name == "Start") {
                startMenu.SetActive(true);
                mainMenu.SetActive(false);
            }
            // Proceed to play tutorial
            else if (GetInteractUIDown() && button.name == "Play tutorial") {
                startMenu.SetActive(false);
                quitToEnterGameMenu.SetActive(true);
                gameplayMode = true;

                // Enable the scripts involving with the gameplay
                hand.GetComponent<SelectWeapon>().enabled = true;
                hand.GetComponent<CharacterMovement>().enabled = true;
                hand.GetComponent<Shooter>().enabled = true;

                playerUI.SetActive(true);
                Time.timeScale = 1;
            }
            // Proceed to the village scene
            else if (GetInteractUIDown() && (button.name == "Skip" || button.name == "Enter Game")) {
                startMenu.SetActive(false);
                gameplayMode = true;
                Time.timeScale = 1;
            } else if (GetInteractUIDown() && button.name == "Resume" && isPause) {
                ResumeGame();
            }
            // Select 'Option' menu from main menu
            else if (GetInteractUIDown() && button.name == "Option" && !gameplayMode) {
                optionMenu.SetActive(true);
                mainMenu.SetActive(false);
            }
            // Select 'Option' menu from game play mode
            else if (GetInteractUIDown() && button.name == "Option" && gameplayMode) {
                optionMenu.SetActive(true);
                pauseMenu.SetActive(false);
            }
            // Select 'Technique' button
            else if (GetInteractUIDown() && button.name == "Technique") {
                if (hand.GetComponent<SelectWeapon>().isHOMER) {
                    techniqueText.text = simRayCastTech;
                    hand.GetComponent<SelectWeapon>().isHOMER = false;
                    hand.GetComponent<SelectWeapon>().isSimRaycast = true;
                } else if (hand.GetComponent<SelectWeapon>().isSimRaycast) {
                    techniqueText.text = homerTech;
                    hand.GetComponent<SelectWeapon>().isHOMER = true;
                    hand.GetComponent<SelectWeapon>().isSimRaycast = false;
                }
            }
            // Back to main menu when the player has not started played game yet
            else if (GetInteractUIDown() && button.name == "Back" && !gameplayMode) {
                mainMenu.SetActive(true);
                optionMenu.SetActive(false);
            }
            // Back to pause menu from game play mode
            else if (GetInteractUIDown() && button.name == "Back" && gameplayMode) {
                pauseMenu.SetActive(true);
                optionMenu.SetActive(false);
            }
            // Select 'Exit'
            else if (GetInteractUIDown() && button.name == "Exit") {
                Application.Quit();
            }
        }
    }

    // Handle raycast events on the button of the menu
    public void RayCastHandler() {
        // Set the ray casting from the controller to the weapon
        LineRenderer ray = raySelect.GetComponent<LineRenderer>();
        ray.startWidth = rayWidth;
        ray.endWidth = rayWidth;
        ray.SetPosition(0, transform.position);
        ray.SetPosition(1, transform.position + transform.forward * lineDistance);

        if (!isPause && !gameplayMode) {
            raySelect.SetActive(true);
        }
        else if (isPause && gameplayMode) {
            raySelect.SetActive(true);
        }
        else if (!isPause && gameplayMode) {
            raySelect.SetActive(false);
        }

        RaycastHit hitInfo;                                                     // Information of the object hit by the raycast
        
        // Player press the trigger to select UI
        bool hit = Physics.Raycast(hand.transform.position, hand.transform.forward, out hitInfo, grabRange);

        // If the raycast hits something
        if (hit) {
            // Get the game object that hit by the raycast
            hitObject = hitInfo.collider.gameObject;
            // If hit object is the button named Cube i.e. button
            if (hitObject.name == "Cube" && !hand.GetComponent<SelectWeapon>().isAttached) {
                button = hitObject.transform.parent.gameObject;                 // Pass button object reference
            } else {
                // Clear the reference
                button = null;
                hitObject = null;
            }
        } else {
            // Clear the reference
            button = null;
            hitObject = null;
        }
    }

    public bool GetInteractUIDown() {
        return SteamVR_Input._default.inActions.InteractUI.GetStateDown(hand.handType);
    }

    public bool GetPauseDown() {
        return SteamVR_Input._default.inActions.Pause.GetStateDown(hand.handType);
    }

    private void ResumeGame() {
        isPause = false;
        pauseMenu.SetActive(false);

        // Enable the scripts involving with the gameplay
        hand.GetComponent<SelectWeapon>().enabled = true;
        hand.GetComponent<CharacterMovement>().enabled = true;
        hand.GetComponent<Shooter>().enabled = true;

        playerUI.SetActive(true);
        Time.timeScale = 1;
    }
}
