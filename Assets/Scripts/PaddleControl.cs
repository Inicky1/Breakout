using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class PaddleControl : MonoBehaviour
{
    //serialized fields
    
    [SerializeField] private float paddleSpeed = 10f;
    [SerializeField] private float reflectingForce = 1f;
    [SerializeField] private float maxBallVelocity = 15f;
    [SerializeField] private float constantBallSpeed = 12f;
    [SerializeField] private float maxReflectionAngleDeg = 75f;
    [SerializeField] private bool useConstantBallSpeed;
    [SerializeField] private Rigidbody ballRb;
    [SerializeField] private InputContainer input;

    private Rigidbody _paddle;

    public float a;
    public float b;
    public TMP_Text equation;
    
    public GameObject number;
    public TMP_Text fallingNumber;

    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNumber", 1, 3);
        _paddle = GetComponent<Rigidbody>();
        input.Current.InGame.Move.performed += context => OnMove(context.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnNumber()
    {
        float x = Mathf.Ceil(Random.Range(0f, 100f));
        fallingNumber.SetText(x.ToString());
        Console.WriteLine("Spawned number");
        Vector3 pos = new Vector3(Random.Range(-30f, 30f), Random.Range(50f, 100f), 0);
        Instantiate(number);
    }
    
    private void OnMove(Vector2 input)
    {
        _paddle.velocity = new Vector3(input.x * paddleSpeed, 0, 0);
    }

    private void FixedUpdate()
    {
        if (useConstantBallSpeed)
        {
            ballRb.velocity = constantBallSpeed * (ballRb.velocity.normalized);
        }
        UpdateEquationPosition();
    }

    private void UpdateEquationPosition()
    {
        // GameObject.FindGameObjectWithTag("Equation").GetComponent(RectTransform).transform.position.x

    }
    private void OnCollisionEnter(Collision collision)
    {
        updateEquation();
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

    public void updateEquation()
    {
        a = Mathf.Ceil(Random.Range(0f, 50f));
        b = Mathf.Ceil(Random.Range(0f, 50f));
        equation.SetText( a + " + " + b + " = ?");
    }


}
