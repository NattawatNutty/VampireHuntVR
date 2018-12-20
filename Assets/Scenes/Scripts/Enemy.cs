using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The required scripts for menu UI
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    // Variable part //
    public int maxHP;                       // maximum HP of the enemy
    public int currentHP;                   // Current HP of the enemy
    public float speed = 1.75f;             // Movement speed of the enemy (default: 1.75)
    public int damage;                      // Damage dealt to the player
    public Material hurt;                   // Material when the enemy hurts
    public Transform player;

    // Use this for initialization
    void Start () {
        tag = "Enemy";
        currentHP = maxHP;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
        agent.speed = speed;

        if (currentHP <= 0) {
            agent.speed = 0;
            GetComponent<Renderer>().material.color = Color.black;
            Destroy(gameObject, 2);
        }
	}
}
