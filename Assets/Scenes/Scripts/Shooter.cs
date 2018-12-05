using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;



public class Shooter : MonoBehaviour {

    // Use this for initialization
    public GameObject projectile;
    public Hand hand;

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
        if (gameObject.GetComponent<SelectWeapon>().isAttached)
        {
            // Get weapon information
            var weapon = GetComponent<SelectWeapon>().weapon;
            var weaponInfo = weapon.GetComponent<Weapon>();

            if (getShootTrigger())
            {
                weaponInfo.remainingAmmo--;
            }
        }
    }

    public bool getShootTrigger()
    {
        return SteamVR_Input._default.inActions.SelectWeapon.GetState(hand.handType);
    }
}
