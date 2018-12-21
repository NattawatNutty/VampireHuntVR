using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    // Attach this script to the player

    // Variable Part //
    public Vector3 firstPos;
    public Quaternion firstRot;
    public GameObject target;
    public float maxHealth = 100f;                                          // Maximum health point of the player
    public float currentHP;                                                 // Current health point of the player
    public Text healthText;                                                 // Text indicating current HP in number
    public float lastHit;
    public bool isGameOver = false;

    public float startTime = 180f;                                          // Total time for the player to play the game in second (default: 180 seconds)
    public Text timerText;                                                  // Text indicating how much time left for this game

    public GameObject weapon;                                               // Current holding weapon
    public Text noWeaponText;                                               // Text indicating the player has no current holding weapon
    public Text maxAmmoText;                                                // Text indicating maximum ammo of the current holding weapon
    public Text remainingAmmoText;                                          // Text indicating remaining ammo of the current holding weapon

    // Use this for initialization
    void Start () {
        currentHP = maxHealth;
        healthText.text = currentHP.ToString();

        // Set to inactive until the player select a weapon
        maxAmmoText.gameObject.SetActive(false);
        remainingAmmoText.gameObject.SetActive(false);
        firstPos = transform.position;
        firstRot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        // Timer update //
        float remainingTime = startTime - Time.fixedTime;

        string min = Mathf.FloorToInt(remainingTime / 60).ToString();
        string sec = Mathf.FloorToInt(remainingTime % 60).ToString("00");
        timerText.text = min + ":" + sec;                                   // Pass the timer to the text UI

        // Weapon/ammo update //
        // Get selection information from the right hand controller
        SelectWeapon selectInfo = GetComponentInChildren<SelectWeapon>();
        bool isAttached = selectInfo.isAttached;

        if(remainingTime <= 0 || currentHP <= 0) {
            isGameOver = true;
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies) {
                Destroy(enemy);
            }
        }

        // If the player carry a weapon
        if (selectInfo != null && isAttached) {
            Weapon weapon = selectInfo.weapon.GetComponent<Weapon>();       // Get weapon information

            // Set no weapon text invisible
            noWeaponText.gameObject.SetActive(false);
            // Set ammo indication UI active
            maxAmmoText.gameObject.SetActive(true);
            remainingAmmoText.gameObject.SetActive(true);
            
            maxAmmoText.text = weapon.maxAmmo.ToString();                   // Pass the maximum ammo to the text UI
            remainingAmmoText.text = weapon.remainingAmmo.ToString();       // Pass the remaining ammo to the text UI
        } else {
            // Set text UI inactive again since the player release the weapon
            maxAmmoText.gameObject.SetActive(false);
            remainingAmmoText.gameObject.SetActive(false);
            // Bring back no weapon text
            noWeaponText.gameObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision) {
        // Get the game object that hits the player
        target = collision.gameObject;

        // If the target is the enemy
        if (target.tag == "Enemy") {
            // Get enemy information
            Enemy enemyInfo = target.GetComponent<Enemy>();
            // if the enemy is still alive, decrease the current HP
            if (currentHP > 0 && Time.time > enemyInfo.hitInterval + lastHit) {
                currentHP -= enemyInfo.damage;
                healthText.text = currentHP.ToString();
                lastHit = Time.time;
            }
        }
    }
}
