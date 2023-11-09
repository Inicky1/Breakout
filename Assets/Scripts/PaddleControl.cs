using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaddleControl : MonoBehaviour
{
    //serialized fields
    
    [SerializeField] private float paddleSpeed = 10f;
    [SerializeField] private float reflectingForce = 1f;
    [SerializeField] private float maxBallVelocity = 15f;
    [SerializeField] private float constantBallSpeed = 12f;
    [SerializeField] private float maxReflectionAngleDeg = 75f;
    [SerializeField] private Rigidbody ballRb;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + new Vector3(horizontal * paddleSpeed * Time.deltaTime, 0, 0);

            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRb)
            {
                //get the contact point and paddle position
                Vector3 hitPoint = collision.GetContact(0).point;
                Vector3 paddleCenter = new Vector3(transform.position.x, transform.position.y);
                
                //angle of reflection is based on where the ball hits the paddle
                float offset = hitPoint.x - paddleCenter.x;
                float halfPaddleWidth = GetComponent<Collider>().bounds.size.x / 2;
                float normalizedOffset = offset / halfPaddleWidth;
                
                //angle is set as a percentage of maxReflectionAngleDeg
                float reflectionAngleDeg = normalizedOffset * maxReflectionAngleDeg;
                
                //Convert to radians for trigonometry
                float reflectionAngleRad = reflectionAngleDeg * Mathf.Deg2Rad;

                Vector3 direction = new Vector3(Mathf.Sin(reflectionAngleRad), Mathf.Cos(reflectionAngleRad));
                
                ballRb.AddForce(ballRb.velocity.magnitude * reflectingForce * direction, ForceMode.VelocityChange);

            }
        }
    }

    public void SetNewBallRigidBody()
    {
        ballRb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        ballRb.velocity = Vector3.zero;
        ballRb.useGravity = false;
        Invoke(nameof(InitialDownPush), 2f);
    }
    
    
    public void InitialDownPush()
    {
        ballRb.useGravity = true;
        ballRb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), -1f).normalized * 5f, ForceMode.Impulse);
    }
}
