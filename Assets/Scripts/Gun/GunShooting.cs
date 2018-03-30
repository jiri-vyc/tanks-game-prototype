using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunShooting : MonoBehaviour {

    public int m_PlayerNumber;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public float m_ShootingDelay;       // gun initial delay
    public float m_TimeBetweenShots;    // gun rate
    public float m_ShotSpeed;           // speed of the projectile
    protected string m_FireButton;      // button to fire
    protected float m_timer;

    protected abstract void Fire();

    private void Start()
    {
        m_PlayerNumber = GetComponentInParent<PlayerGunControl>().m_playerNumber;
        m_FireButton = "Fire" + m_PlayerNumber;
        m_timer = m_TimeBetweenShots + 0.01f;
    }

    protected void Update()
    {
        m_timer += Time.deltaTime;
        if (Input.GetButtonDown(m_FireButton) && m_timer >= m_TimeBetweenShots && Time.timeScale != 0)
        {
            Fire();
            m_timer = 0.0f;
        }
    }

}
