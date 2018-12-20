using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

// The required scripts for weapon
[RequireComponent(typeof(SelectWeapon))]

public class Shooter : MonoBehaviour
{
    // Attach this script to right hand VR controller

    // Variable part //
    private float rayWidth = 0.01f;                                             // The width of the ray
    private float lineDistance = 20f;                                           // Distance of the raycase
    private float lastShot = 1f;                                                // The last shot of the weapon for fire rate counting (1 second for delay)

    public Hand hand;
    public GameObject raySelect;
    public GameObject weapon;

	void Start () {
        if (hand == null)
        {
            hand = this.GetComponent<Hand>();                                   // Get the Hand component
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Check whether the user has obtained a weapon in hand
        if (GetComponent<SelectWeapon>().isAttached)
        {
            // Get weapon information
            weapon = GetComponent<SelectWeapon>().weapon;
            Weapon weaponInfo = weapon.GetComponent<Weapon>();

            // When the player runs out of ammo, notify to release current weapon
            if (!weaponInfo.isPickable)
            {
                return;
            }

            // The next bullet will wait due to the fire rate of the weapon
            if (getShootTrigger() && Time.time > weaponInfo.fireRate + lastShot)
            {
                // Instantiate a bullet from the prefab
                GameObject aBullet = Instantiate(weaponInfo.bulletPrefab, weaponInfo.bulletPos.position, weaponInfo.bulletPos.rotation);
                aBullet.SetActive(true);
                // Calculate the velocity of the bullet with the velocity of the weapon
                aBullet.GetComponent<Rigidbody>().isKinematic = false;
                aBullet.GetComponent<Rigidbody>().velocity = aBullet.transform.forward * weaponInfo.velocity;
                weaponInfo.remainingAmmo--;                                     // Decrease ammo by 1
                lastShot = Time.time;                                           // Store new time for the latest shot
            }
        }
    }

    public bool getShootTrigger()
    {
        return SteamVR_Input._default.inActions.Shoot.GetState(hand.handType);
    }

    public void RaycaseHandler() {
        // Set the ray casting from weapon
        LineRenderer ray = raySelect.GetComponent<LineRenderer>();
        ray.startWidth = rayWidth;
        ray.endWidth = rayWidth;
        ray.SetPosition(0, weapon.transform.position);
        ray.SetPosition(1, weapon.transform.position + weapon.transform.forward * lineDistance);
    }
}
