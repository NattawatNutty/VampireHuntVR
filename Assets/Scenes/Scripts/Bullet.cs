using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    //// Variable part ////
    public Weapon weaponInfo;
    public int damage;

    // Use this for initialization
    void Start () {
        tag = "Bullet";
        weaponInfo = transform.parent.GetComponent<Weapon>();
        damage = weaponInfo.damage;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter(Collision collision) {
        var col = collision.gameObject;
        if (col.tag == "Enemy") {
            var enemyInfo = col.GetComponent<Enemy>();
            if(enemyInfo.currentHP > 0) {
                enemyInfo.currentHP -= damage;
            }
        }
    }
}
