using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGunShooting : GunShooting {

    protected override void Fire() 
    {
        // Instantiate and launch the shell.

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_ShotSpeed * m_FireTransform.forward;
       
    }
}
