using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The required scripts for menu UI
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    // Attach this script to the enemy

    // Variable part //
    public int maxHP;                       // maximum HP of the enemy
    public int currentHP;                   // Current HP of the enemy
    public float speed = 1.75f;             // Movement speed of the enemy (default: 1.75)
    public int damage;                      // Damage dealt to the player
    public bool isHurt;                     // Is the enemy hurt by the bullet ?

    public Material hurtMaterial;           // Material used for hurt enemy
    public Material deadMaterial;           // Material used for dead enemy
    public Material normalMaterial;         // Normal material

    public Transform player;                // Position of the player as the target of the enemy

    // Use this for initialization
    private void Start () {
        tag = "Enemy";
        currentHP = maxHP;
    }
	
	private void FixedUpdate () {
        // Get NavMeshAgent component
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        // Set the destination of the enemy to be the position of the player
        agent.destination = player.position;
        agent.speed = speed;

        // If the enemy is damaged from the bullet
        if (isHurt) {
            GetComponentInChildren<MeshRenderer>().material = hurtMaterial;
        }
        else {
            GetComponentInChildren<MeshRenderer>().material = normalMaterial;
        }

        // If the enemy is dead, destroy the enemy with the delay of 2 seconds
        if (currentHP <= 0) {
            agent.speed = 0;
            GetComponentInChildren<MeshRenderer>().material = deadMaterial;
            Destroy(gameObject, 2);
        }
	}
}
