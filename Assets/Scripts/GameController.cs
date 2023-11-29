using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Canvas gameOverScreen;
    [SerializeField] private GameObject ballObject;

    [SerializeField] private PaddleControl paddleControl;

    // Start is called before the first frame update
    private Renderer ballRenderer;
   
    private void Start()
    {
     ballObject = GameObject.FindGameObjectWithTag("Ball");
        paddleControl = FindObjectOfType<PaddleControl>();

        ballRenderer = GetComponent<Renderer>();
        InvokeRepeating("CheckForEndOfGame", 20, 3);

    }

    // Update is called once per frame
    private void Update()
    {

        // if (lives == 0)
        // {
        //     gameOverScreen.GetComponent<Canvas>().enabled = true;
        //     Time.timeScale = 0;
        // }
    }



    private void OnBecameInvisible()
    {

        LooseALife();
    }

    private void LooseALife()
    {
        lives--;
        paddle.GetComponent<MeshRenderer>().enabled = false;
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
            Debug.Log(ballObject.transform.position);
        }
        else
        {
            Instantiate(ballPrefab, ballStartPos, Quaternion.identity);
        }
         paddleControl.ChangeUseConstantBallSpeed(true);

        paddle.transform.position = paddleStartPos;
        ballObject.GetComponent<MeshRenderer>().enabled = true;
        paddle.GetComponent<MeshRenderer>().enabled = true;
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