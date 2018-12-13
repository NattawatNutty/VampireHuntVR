﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //// Variable part ////
    public int maxHP;                       // maximum HP of the enemy
    public int currentHP;                   // Current HP of the enemy
    public int speed;                       // Movement speed of the enemy
    public int damage;                      // Damage dealt to the player
    public Material hurt;                   // Material when the enemy hurts

    // Use this for initialization
    void Start () {
        tag = "Enemy";
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(currentHP <= 0) {
            GetComponent<Renderer>().material.color = Color.black;
            Destroy(gameObject, 2);
        }
	}
}
