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
    private LineRenderer ray;                                                   // LineRenderer component
    private float lineDistance = 5f;                                            // Distance of the raycast showing to the player
    private Weapon weaponInfo;                                                  // Weapon information
    private MenuUI menuInfo;
    private SteamVR_Action_Vector2 touchpadCoor;                                // Touchpad action
    private Vector3 movement = new Vector3();                                   // Movement for simple raycasting to drag the gun to the hand
    private Vector2 touchPos;                                                   // Touchpad position on the left controller
                                                  
    public GameObject raySelect;                                                // LineRenderer for casting the ray
    public Color startColor = Color.green;                                      // Initial color of the line (default: green)
    public Color endColor = Color.red;                                          // End color of the line (default: red)

    public bool isAttached = false;                                             // Is a weapon attach to a hand?
    public bool isHOMER = true;                                                 // Is current technique HOMER? (default: true)
    public bool isSimRaycast = false;                                           // Is current technique simeple raycasting? (default: false)
    public float grabRange = 50f;                                               // The range of the ray
    public Hand leftHand;                                                       // The hand this script attached to
    public Hand rightHand;                                                      // The hand this script attached to
    public GameObject weapon;                                                   // The current selected weapon

    // Use this for initialization
    private void Start()
    {
        if (rightHand == null)
        {
            rightHand = this.GetComponent<Hand>();                              // Get the Hand component
            leftHand = rightHand.otherHand;
        }

        // Set the ray casting from the controller to the weapon
        ray = raySelect.GetComponent<LineRenderer>();
        ray.material = new Material(Shader.Find("Sprites/Default"));            // Represent how the shade of the line is
        ray.startWidth = rayWidth;
        ray.endWidth = rayWidth;
        ray.startColor = Color.green;
        ray.endColor = Color.red;

        menuInfo = GetComponent<MenuUI>();
    }

    private void FixedUpdate()
    {
        ray.SetPosition(0, transform.position);
        ray.SetPosition(1, transform.position + transform.forward * lineDistance);

        // If the player presses the grip while holding the weapon, detach the weapon
        if (isAttached)
        {
            if (getReleaseWeapon() || transform.root.GetComponent<PlayerUI>().isGameOver)
            {
                isAttached = false;
                weaponInfo.isAttached = false;
                weaponInfo.isHitByRaycast = false;
                weapon.GetComponent<Rigidbody>().isKinematic = false;
                weapon.transform.parent = null;                                 // Detach the object from the hand
                weapon = null;                                                  // Clear the weapon

                ray.SetPosition(0, transform.position);
                ray.SetPosition(1, transform.position + transform.forward * lineDistance);
            }

            else if (isSimRaycast) {
                // Touchpad input from the player
                touchpadCoor = SteamVR_Input._default.inActions.TouchPos;
                // Touch pad position
                touchPos = touchpadCoor.GetAxis(leftHand.handType);

                movement = new Vector3(0, 0, touchPos.y / 40f);
                weapon.transform.localPosition += movement;

                float distance = Vector3.Distance(transform.position, weapon.transform.position);

                if(distance < 0.2) {
                    weapon.transform.position = transform.position;
                }
            }
            return;
        }

        RaycastHit hitInfo;                                                     // Information of the object hit by the raycast

        // Check whether the raycast from the right controller hits a collider
        bool hit = Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hitInfo, grabRange);

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

                    ray.SetPosition(0, weapon.transform.position);
                    ray.SetPosition(1, weapon.transform.position + weapon.transform.forward * lineDistance);
                }
                // Simple raycast technique //
                else if (getSelectWeaponDown() && !isAttached && isSimRaycast) {
                    isAttached = true;                                          // The player is holding the weapon, not allowing to carry another one
                    weapon.GetComponent<Rigidbody>().isKinematic = true;        // Ignore the gravity
                    weaponInfo.isAttached = true;
                    weaponInfo.isHitByRaycast = false;
                    weapon.transform.rotation = transform.rotation;
                    weapon.transform.parent = transform;                        // Set parent of the weapon to the hand

                    ray.SetPosition(0, weapon.transform.position);
                    ray.SetPosition(1, weapon.transform.position + weapon.transform.forward * lineDistance);
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
        return SteamVR_Input._default.inActions.SelectWeapon.GetStateDown(rightHand.handType);
    }

    public bool getReleaseWeapon()
    {
        return SteamVR_Input._default.inActions.ReleaseWeapon.GetStateDown(rightHand.handType);
    }
}