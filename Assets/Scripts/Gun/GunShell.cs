using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShell : MonoBehaviour
{

    public float m_Damage = 100f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickable")
        {
            return;
        }

        Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();

        for (int i = 0; i < 1; i++)
        {
            if (!targetRigidbody) break;

            PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth>();

            if (!targetHealth) break;

            targetHealth.TakeDamage(m_Damage);
        }

        Destroy(gameObject);
    }
}
