using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NumberBehavior : MonoBehaviour
{
    private GameObject number;
    // Start is called before the first frame update
    void Start()
    {
        number = GameObject.FindGameObjectWithTag("Number");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            Debug.Log("NumberCollide");
            number.GetComponent<Rigidbody>().AddForce(10f, 10f, 10f);
        }

    }
}
