using System;
using System.Collections;
using System.Collections.Generic;
using PowerUp;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    [SerializeField] private TMP_Text livesTextInfo;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 ballStartPos = new Vector3(-0.67f, 22f, 0f);
    [SerializeField] private Vector3 paddleStartPos = new Vector3(1.28f, 1f, 0f);
    [SerializeField] private PaddleControl paddle;

    [SerializeField] private GameObject ballObject;

    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private PaddleControl paddleControl;


    //PowerUp
    [SerializeField] private GameObject expendPanel;
    [SerializeField] private GameObject shrinkPanel;

    // Start is called before the first frame update
    private Renderer ballRenderer;

    public List<PowerUpProps> PowerUp { get; } = new List<PowerUpProps>();

    private void Start()
    {
        ballObject = GameObject.FindGameObjectWithTag("Ball");
        paddleControl = FindObjectOfType<PaddleControl>();

        ballRenderer = GetComponent<Renderer>();
        InvokeRepeating("CheckForEndOfGame", 20, 3);
        PowerUp.Add(new PowerUpProps(PowerUpType.ExpandPaddle, expendPanel));
        PowerUp.Add(new PowerUpProps(PowerUpType.ShrinkPaddle, shrinkPanel));
    }

    // Update is called once per frame
    private void Update()
    {
        if (lives == 0)
        {
            gameOverScreen.Setup(paddleControl._points);
        }
    }


    private void OnBecameInvisible()
    {
        LooseALife();
    }

    private void LooseALife()
    {
        lives--;
        // paddle.GetComponent<MeshRenderer>().enabled = false;
        if (lives > 0)
        {
            ResetBallAndPaddle();
        }
    }

    public void ResetBallAndPaddle()
    {
        var ballRb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        ballObject.GetComponent<MeshRenderer>().enabled = false;
        if (ballObject != null)
        {
            
            paddleControl.ChangeUseConstantBallSpeed(false);
            ballObject.transform.position = ballStartPos;
            ballRb.velocity = Vector3.zero; 
            
        }
        else
        {
            Instantiate(ballPrefab, ballStartPos, Quaternion.identity);
        }

        paddleControl.ChangeUseConstantBallSpeed(true);

        paddle.transform.position = paddleStartPos;
        ballObject.GetComponent<MeshRenderer>().enabled = true;
        // paddle.GetComponent<MeshRenderer>().enabled = true;
        paddle.SetNewBallRigidBody();
    }

    public void CheckForEndOfGame()
    {
    }

    public void SpawnNewBall()
    {
        Instantiate(ballPrefab, ballStartPos, Quaternion.identity);
        paddle.SetNewBallRigidBody();
    }
}