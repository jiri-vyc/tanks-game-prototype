using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToPickUp : MonoBehaviour {

    public int m_weaponType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerGunControl playerGunControl = other.GetComponentInChildren<PlayerGunControl>();
        if (playerGunControl)
        {
            playerGunControl.PickupWeapon(m_weaponType);
            Destroy(gameObject);
        }
    }
}
