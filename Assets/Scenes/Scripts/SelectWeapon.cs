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
    //// Variable part ////
    //private SteamVR_Action_Boolean grabAction;                                  // Grab action
    private float rayWidth = 0.05f;                                             // The width of the ray
    public LineRenderer raySelect;                                              // LineRenderer for casting the ray
    public bool isAttached = false;                                             // Is a weapon attach to a hand?
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
        raySelect.enabled = false;
    }

    private void FixedUpdate()
    {

        // If the player press the grip while holding the weapon, detach the weapon
        if (isAttached)
        {
            if (getReleaseWeapon())
            {
                //Debug.Log("Weapon released");
                isAttached = false;
                weapon.GetComponent<Weapon>().isHit = false;
                weapon.GetComponent<Rigidbody>().isKinematic = false;
                weapon.GetComponent<Weapon>().isAttached = false;
                weapon.transform.parent = null;                                 // Detach the object from the hand
                weapon = null;                                                  // Clear the weapon
            }
            return;
        }

        RaycastHit hitInfo;                                                     // Information of the object hit by the raycast

        // Check whether the raycast from the right controller hits a collider
        bool hit = Physics.Raycast(hand.transform.position, hand.transform.forward, out hitInfo, grabRange);
        Debug.DrawRay(hand.transform.position, hand.transform.forward);
        // If the raycast hits something
        if (hit)
        {
            //Debug.Log("Hit");

            // If the raycast hit the weapon
            if (hitInfo.collider.gameObject.tag == "Weapon" && hitInfo.collider.gameObject.GetComponent<Weapon>().remainingAmmo > 0)
            {
                //Debug.Log("Hit the weapon");
                weapon = hitInfo.collider.gameObject;                           // Obtain the weapon game object
                var lineDistance = Vector3.Distance(transform.position, weapon.transform.position);
                weapon.GetComponent<Weapon>().isHit = true;                     // The weapon is hit by the raycast

                // Set the ray casting from the controller to the weapon
                raySelect.enabled = true;
                raySelect.startWidth = rayWidth;
                raySelect.endWidth = rayWidth;
                raySelect.SetPosition(0, transform.position);
                raySelect.SetPosition(1, transform.position + transform.forward * lineDistance);

                // If the player press the trigger, attach the weapon to the player's right hand
                if (getSelectWeapon() && !isAttached)
                {
                    //Debug.Log("Weapon selected");
                    isAttached = true;                                          // The player is holding the weapon, not allowing to carry another one
                    raySelect.enabled = false;
                    weapon.GetComponent<Rigidbody>().isKinematic = true;        // Ignore the gravity
                    weapon.GetComponent<Weapon>().isAttached = true;
                    weapon.transform.position = transform.position;
                    weapon.transform.rotation = transform.rotation;
                    weapon.transform.parent = transform;                        // Set parent of the weapon to the hand
                }
            }
            else
            {
                if(weapon != null)
                {
                    weapon.GetComponent<Weapon>().isHit = false;                // Clear the hit flag
                }
                raySelect.enabled = false;
                weapon = null;                                                  // Clear the weapon
            }
        }
    }

    public bool getSelectWeapon()
    {
        return SteamVR_Input._default.inActions.SelectWeapon.GetState(hand.handType);
    }

    public bool getReleaseWeapon()
    {
        return SteamVR_Input._default.inActions.ReleaseWeapon.GetStateDown(hand.handType);
    }
}