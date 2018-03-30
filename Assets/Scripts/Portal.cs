using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject otherPortal;
    public float delayTime = 1.0f;

    private float currentStayTime = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        //other.gameObject.transform.position = otherPortal.transform.position;
    }

    void OnTriggerStay(Collider other)
    {
        currentStayTime += Time.deltaTime;

        if (currentStayTime >= delayTime)
        {
            other.gameObject.GetComponent<Rigidbody>().position = otherPortal.transform.position;
            currentStayTime = 0.0f;
        }
    }
}
