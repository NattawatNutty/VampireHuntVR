// ITCS496
// Team members
// Nattawat         Puakpaiboon 5888158

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SelectWeapon : MonoBehaviour
{
    // Attach this script to right hand VR controller

    // Variable part //
    private float rayWidth = 0.01f;                                             // The width of the ray
    private LineRenderer ray;
    private float lineDistance = 20f;
    private Weapon weaponInfo;

    public GameObject raySelect;                                                // LineRenderer for casting the ray
    public bool isAttached = false;                                             // Is a weapon attach to a hand?
    public bool isHOMER = true;
    public bool isSimRaycast = false;
    public float grabRange = 50f;                                               // The range of the ray
    public Hand hand;                                                           // The hand this script attached to
    public GameObject weapon;                                                   // The current selected weapon

    // Use this for initialization
    private void Start()
    {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                                   // Get the Hand component
        }
    }

    private void FixedUpdate()
    {
        // Set the ray casting from the controller to the weapon
        LineRenderer ray = raySelect.GetComponent<LineRenderer>();
        ray.startWidth = rayWidth;
        ray.endWidth = rayWidth;
        ray.SetPosition(0, transform.position);
        ray.SetPosition(1, transform.position + transform.forward * lineDistance);

        if (!isAttached) {
            raySelect.SetActive(true);
        }
        else {
            raySelect.SetActive(false);
        }

        // If the player press the grip while holding the weapon, detach the weapon
        if (isAttached)
        {
            if (getReleaseWeapon())
            {
                //Debug.Log("Weapon released");
                isAttached = false;
                weaponInfo.isAttached = false;
                weaponInfo.isHitByRaycast = false;
                weapon.GetComponent<Rigidbody>().isKinematic = false;
                weapon.transform.parent = null;                                 // Detach the object from the hand
                weapon = null;                                                  // Clear the weapon
            }
            return;
        }

        RaycastHit hitInfo;                                                     // Information of the object hit by the raycast

        // Check whether the raycast from the right controller hits a collider
        bool hit = Physics.Raycast(hand.transform.position, hand.transform.forward, out hitInfo, grabRange);

        // If the raycast hits something
        if (hit)
        {
            // If the raycast hit the weapon
            if (hitInfo.collider.gameObject.tag == "Weapon" && hitInfo.collider.gameObject.GetComponent<Weapon>().isPickable)
            {
                //Debug.Log("Hit the weapon");
                weapon = hitInfo.collider.gameObject;                           // Obtain the weapon game object
                weaponInfo = weapon.GetComponent<Weapon>();
                weaponInfo.isHitByRaycast = true;

                // If the player press the trigger, attach the weapon to the player's right hand
                // HOMER technique //
                if (getSelectWeaponDown() && !isAttached && isHOMER)
                {
                    isAttached = true;                                          // The player is holding the weapon, not allowing to carry another one
                    weapon.GetComponent<Rigidbody>().isKinematic = true;        // Ignore the gravity
                    weaponInfo.isAttached = true;
                    weaponInfo.isHitByRaycast = false;
                    weapon.transform.position = transform.position;
                    weapon.transform.rotation = transform.rotation;
                    weapon.transform.parent = transform;                        // Set parent of the weapon to the hand
                }
                // Simple raycast technique //
                else if (getSelectWeaponDown() && !isAttached && isSimRaycast) {
                    isAttached = true;                                          // The player is holding the weapon, not allowing to carry another one
                    weapon.GetComponent<Rigidbody>().isKinematic = true;        // Ignore the gravity
                    weaponInfo.isAttached = true;
                    weaponInfo.isHitByRaycast = false;
                }
            }
            else
            {
                if(weapon != null)
                {
                    weaponInfo.isAttached = false;
                    weaponInfo.isHitByRaycast = false;                          // Clear the hit flag
                }
                weapon = null;                                                  // Clear the weapon
            }
        }
    }

    public bool getSelectWeaponDown()
    {
        return SteamVR_Input._default.inActions.SelectWeapon.GetStateDown(hand.handType);
    }

    public bool getReleaseWeapon()
    {
        return SteamVR_Input._default.inActions.ReleaseWeapon.GetStateDown(hand.handType);
    }
}