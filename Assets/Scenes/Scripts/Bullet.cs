using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Variable part //
    public bool isHit = false;                  // Check whether the bullet hit something
    public Weapon weaponInfo;                   // Weapon information
    public int damage;                          // Damage of each bullet

    // Use this for initialization
    void Start () {
        tag = "Bullet";

        if(transform.parent != null) {
            weaponInfo = transform.parent.GetComponent<Weapon>();
            damage = weaponInfo.damage;         // Pass the damage value from the weapon
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    private IEnumerator OnCollisionEnter(Collision collision) {
        // Get the game object that the bullet hits
        GameObject target = collision.gameObject;

        // If the target is the enemy
        if (target.tag == "Enemy") {
            // Get enemy information
            Enemy enemyInfo = target.GetComponent<Enemy>();
            // if the enemy is still alive, decrease the current HP
            if(enemyInfo.currentHP > 0) {
                enemyInfo.currentHP -= damage;
                enemyInfo.GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(.3f);
                enemyInfo.GetComponent<Renderer>().material.color = Color.white;
                isHit = true;
            }
        }
        // When bullet hits other things or hits the same enemy again (bouncing), the damage will be null
        else if (target.tag != "Enemy" && isHit) {
            damage = 0;
        }
    }
}
