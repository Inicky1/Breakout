using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{

    [SerializeField] private int lives;

    [SerializeField] private TMP_Text livesTextInfo;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 ballStartPos;
    [SerializeField] private Vector3 paddleStartPos;
    [SerializeField] private PaddleControl paddle;
    [SerializeField] private Canvas gameOverScreen;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBall();
        InvokeRepeating("CheckForEndOfGame", 20, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            gameOverScreen.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }
    }

    public void LooseALife()
    {
        lives--;
        paddle.GetComponent<MeshRenderer>().enabled = false;
        if (lives > 0)
        {
            
        }
    }
    
    public void ResetBallAndPaddle()
    {
        if (GameObject.FindGameObjectWithTag("Ball"))
        {
            GameObject.FindGameObjectWithTag("Ball").transform.position = ballStartPos;
        }
        else
        {
            Instantiate(ballPrefab, ballStartPos, Quaternion.identity);
        }

        paddle.transform.position = paddleStartPos;
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
