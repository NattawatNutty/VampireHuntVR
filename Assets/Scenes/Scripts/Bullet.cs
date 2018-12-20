using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The required scripts for weapon
[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour {

    // Attach this script to bullet (must have weapon as the parent)

    // Variable part //
    public bool isHit = false;                  // Does the bullet hit something ?
    public Weapon weaponInfo;                   // Weapon information
    public int damage;                          // Damage of each bullet

    // Use this for initialization
    void Start () {
        tag = "Bullet";

        // Get the parent component (weapon)
        if(transform.parent != null) {
            weaponInfo = transform.parent.GetComponent<Weapon>();
            damage = weaponInfo.damage;         // Pass the damage value from the weapon
            gameObject.SetActive(false);
        }
    }

    // When the bullet collides with something
    private void OnCollisionEnter(Collision collision) {
        // Get the game object that the bullet hits
        GameObject target = collision.gameObject;

        // If the target is the enemy
        if (target.tag == "Enemy") {
            // Get enemy information
            Enemy enemyInfo = target.GetComponent<Enemy>();
            // if the enemy is still alive, decrease the current HP
            if(enemyInfo.currentHP > 0) {
                enemyInfo.currentHP -= damage;
                enemyInfo.isHurt = true;
            }
        }
        // When bullet hits other things or hits the same enemy again (bouncing/lying on the ground), the damage will be null
        else if (target.tag != "Enemy" || isHit) {
            damage = 0;
        }

        isHit = true;
    }

    // When the bullet stops collision with something
    private void OnCollisionExit(Collision collision) {
        // Get the game object that the bullet just hit
        GameObject target = collision.gameObject;

        if (target.tag == "Enemy") {
            // Get enemy information
            Enemy enemyInfo = target.GetComponent<Enemy>();
            enemyInfo.isHurt = false;
        }
    }
}
