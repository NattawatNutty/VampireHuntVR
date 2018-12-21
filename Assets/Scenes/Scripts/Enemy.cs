using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The required scripts for menu UI
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class Enemy : MonoBehaviour
{
    // Attach this script to the enemy

    // Variable part //
    private float materialDelay = 0.5f;     // Time that the enemy display hurt material as the response to the player
    public float hitInterval;
    public int maxHP;                       // maximum HP of the enemy
    public int currentHP;                   // Current HP of the enemy
    public float speed = 1.75f;             // Movement speed of the enemy (default: 1.75)
    public int damage;                      // Damage dealt to the player
    public bool isHurt;                     // Is the enemy hurt by the bullet ?
    public float lastHit;
    public int score;                       // Score the player got from killing the enemies

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

        SkinnedMeshRenderer[] allMeshes = GetComponentsInChildren<SkinnedMeshRenderer>();

        // If the enemy is damaged from the bullet and it is still in material delay time
        if (isHurt && Time.time < lastHit + materialDelay) {
            foreach (SkinnedMeshRenderer mesh in allMeshes) {
                mesh.material = hurtMaterial;
            }
        }
        else {
            foreach (SkinnedMeshRenderer mesh in allMeshes) {
                mesh.material = normalMaterial;
            }
        }

        // If the enemy is dead, destroy the enemy with the delay of 2 seconds
        if (currentHP <= 0) {
            agent.isStopped = true;
            //agent.speed = 0;
            foreach (SkinnedMeshRenderer mesh in allMeshes) {
                mesh.material = deadMaterial;
            }
            Destroy(gameObject, 2);
        }
	}
}
