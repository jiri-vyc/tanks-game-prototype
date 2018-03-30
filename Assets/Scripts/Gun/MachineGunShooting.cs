using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShooting : GunShooting {

    public float m_bulletSpread = 0.15f;
    public int m_numberOfBullets = 6;
    public float m_timeSpread = 0.15f;

    private WaitForSeconds m_waitForSecondsTimeSpread;

    protected void Awake()
    {
        m_waitForSecondsTimeSpread = new WaitForSeconds(m_timeSpread);
    }

    protected override void Fire()
    {
        // Instantiate and launch the shell.
        StartCoroutine( Shoot() );
    }

    private IEnumerator Shoot()
    {
        for (int i = 0; i < m_numberOfBullets; i++)
        {
            float randomSpread = Random.Range(-m_bulletSpread, m_bulletSpread);
            Rigidbody shellInstance = Instantiate(m_Shell, new Vector3(m_FireTransform.position.x, m_FireTransform.position.y, m_FireTransform.position.z), m_FireTransform.rotation) as Rigidbody;
            shellInstance.velocity = m_ShotSpeed * m_FireTransform.forward;
            yield return m_waitForSecondsTimeSpread;
        }
    }


}
