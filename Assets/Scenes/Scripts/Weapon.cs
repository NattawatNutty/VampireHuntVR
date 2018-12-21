using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The required scripts for weapon
[RequireComponent(typeof(Rigidbody))]

public class Weapon : MonoBehaviour
{
    // Attach this script to weapons

    // Variable part //
    private const string OutlineWidthKey = "Outline";
    private const float OutlineWidthValue = 0.05f;              // Width of the outline

    public Material highlightMaterial;                          // Highlight material
    public Material normalMaterial;                             // Normal material

    public bool isHitByRaycast = false;                         // Is this weapon hit by the raycast?
    public bool wasHitByRaycast = false;
    public bool isAttached = false;                             // Is this weapon attached to any hand?
    public bool isPickable = true;                              // Is this weapon pickable?

    public int maxAmmo;                                         // Maximum (default) ammo of the weapon
    public int remainingAmmo;                                   // Remaining ammo of the weapon
    public int damage;                                          // Damage per bullet hit the enemies from the weapon
    public float fireRate;                                      // Fire rate of the weapon in second
    public float velocity;                                      // Velocity of each bullet shot

    public GameObject bulletPrefab;                             // Bullet prefab
    public Transform bulletPos;                                 // Bullet position

    // Use this for initialization
    void Start () {
        tag = "Weapon";
        remainingAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update () {
        if (isHitByRaycast) {
            GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        else if (!isHitByRaycast) {
            GetComponent<MeshRenderer>().material = normalMaterial;
        }

        if (remainingAmmo <= 0) {
            isPickable = false;
        }
    }
}
