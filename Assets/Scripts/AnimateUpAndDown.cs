﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateUpAndDown : MonoBehaviour {

    // Update is called once per frame
    //adjust this to change speed
    public float speed = 5f;
    //adjust this to change how high it goes
    public float height = 0.5f;

    private Vector3 m_originalPosition;

    private void Start()
    {
        m_originalPosition = transform.position;
    }

    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z) * height;
        transform.position = new Vector3(transform.position.x + m_originalPosition.x, transform.position.y + m_originalPosition.y, transform.position.z + m_originalPosition.z);
    }
}
