using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour {

    // Variable Part //
    public float maxHealth = 100f;                                          // Maximum health point of the player
    public static float health;                                             // Current health point of the player

    public GameObject weapon;                                               // Current holding weapon

    public Image healthBar;
    public Image reticle;
    public Text maxAmmoText;                                                // Text indicating maximum ammo of the current holding weapon
    public Text remainingAmmoText;                                          // Text indicating remaining ammo of the current holding weapon

    // Use this for initialization
    void Start () {
        healthBar = GetComponent<Image>();
        health = maxHealth;

        // Set to inactive until the player select a weapon
        maxAmmoText.gameObject.SetActive(false);
        remainingAmmoText.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //healthBar.fillAmount = health / maxHealth;

        // Get selection information from the right hand controller
        SelectWeapon selectInfo = GetComponentInChildren<SelectWeapon>();
        bool isAttached = selectInfo.isAttached;

        // If the player carry a weapon
        if (selectInfo != null && isAttached) {
            Weapon weapon = selectInfo.weapon.GetComponent<Weapon>();       // Get weapon information

            // Set text UI active
            maxAmmoText.gameObject.SetActive(true);
            remainingAmmoText.gameObject.SetActive(true);
            maxAmmoText.text = weapon.maxAmmo.ToString();                   // Pass the maximum ammo to the text UI
            remainingAmmoText.text = weapon.remainingAmmo.ToString();       // Pass the remaining ammo to the text UI
        } else {
            // Set text UI inactive again since the player release the weapon
            maxAmmoText.gameObject.SetActive(false);
            remainingAmmoText.gameObject.SetActive(false);
        }
    }
}
