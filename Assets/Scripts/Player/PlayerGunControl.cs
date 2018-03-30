using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunControl : MonoBehaviour {

    public static int BASIC_GUN = 0;
    public static int MACHINE_GUN = 1;
    public static int ROCKET_LAUNCHER = 2;

    protected int m_selectedWeapon;
    public int m_playerNumber = 1;
    public bool[] m_pickedWeapons;

    private string m_playerSwitchButton;

    // Use this for initialization
    void Start()
    {
        m_playerSwitchButton = "Switch" + m_playerNumber;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(m_playerSwitchButton))
        {
            int weaponToSelect = m_selectedWeapon + 1;
            if (weaponToSelect >= transform.childCount)
            {
                weaponToSelect = 0;
            }
            while (!m_pickedWeapons[weaponToSelect])
            {
                weaponToSelect++;
                if (weaponToSelect >= transform.childCount)
                {
                    weaponToSelect = 0;
                }
            }
            SelectWeapon(weaponToSelect);
        }
        
    }

    void SelectWeapon(int weaponNumber)
    {
        transform.GetChild(m_selectedWeapon).gameObject.SetActive(false);
        transform.GetChild(weaponNumber).gameObject.SetActive(true);
        m_selectedWeapon = weaponNumber;
    }

    void DeselectAllWeapons()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public void PickupWeapon(int weaponPickedUp)
    {
        m_pickedWeapons[weaponPickedUp] = true;
        SelectWeapon(weaponPickedUp);
    }

    public void Reset()
    {
        DropAllWeapons();
        m_pickedWeapons[0] = true;
        DeselectAllWeapons();
        SelectWeapon(PlayerGunControl.BASIC_GUN);
    }

    public void DropAllWeapons()
    {
        for (int i = 0; i < m_pickedWeapons.Length; i++)
        {
            m_pickedWeapons[i] = false;
        }
    }

}
