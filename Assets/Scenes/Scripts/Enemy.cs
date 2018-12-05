using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //// Variable part ////
    public int maxHP;                       // maximum HP of the enemy
    public int currentHP;                   // Current HP of the enemy
    public int speed;                       // Movement speed of the enemy
    public int damage;                      // Damage dealt to the player

    // Use this for initialization
    void Start () {
        tag = "Enemy";
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
