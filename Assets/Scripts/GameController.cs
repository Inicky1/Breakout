
using System.Collections.Generic;
using PowerUp;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 ballStartPos = new Vector3(-0.67f, 22f, 0f);
    [SerializeField] private Vector3 paddleStartPos = new Vector3(1.28f, 1f, 0f);
    [SerializeField] private PaddleControl paddle;

    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private PaddleControl paddleControl;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;


    //PowerUp
    [SerializeField] private GameObject expendPanel;
    [SerializeField] private GameObject shrinkPanel;
    [SerializeField] private GameObject magnetPanel;
    [SerializeField] private GameObject explodePanel;
    [SerializeField] private GameObject oneUpPanel;
    [SerializeField] private GameObject extraBallPanel;
    [SerializeField] private GameObject slowBallPanel;
    [SerializeField] private GameObject fastBallPanel;

    // Start is called before the first frame update
    private Renderer ballRenderer;

    public List<PowerUpProps> PowerUp { get; } = new List<PowerUpProps>();
    public bool UseConstantBallSpeed { get; private set; } = true;
    
    private void Start()
    {

        scoreText.text = paddle.points.ToString("0000");
        livesText.text = "Life Left: " + lives;
        paddleControl = FindObjectOfType<PaddleControl>();
        PlayerPrefs.SetInt("isSpeedDown",1);
        PlayerPrefs.SetInt("isShrinkPaddle",1);
        ballRenderer = GetComponent<Renderer>();
        InvokeRepeating("CheckForEndOfGame", 20, 3);
        PowerUp.Add(new PowerUpProps(PowerUpType.ExpandPaddle, expendPanel));
        PowerUp.Add(new PowerUpProps(PowerUpType.ShrinkPaddle, shrinkPanel));
        PowerUp.Add(new PowerUpProps(PowerUpType.Magnet, magnetPanel));
        PowerUp.Add(new PowerUpProps(PowerUpType.Explotion, explodePanel));
        PowerUp.Add(new PowerUpProps(PowerUpType.OneUp, oneUpPanel));
      //  PowerUp.Add(new PowerUpProps(PowerUpType.ExtraBall, extraBallPanel));
        PowerUp.Add(new PowerUpProps(PowerUpType.SpeedUp, fastBallPanel));
        PowerUp.Add(new PowerUpProps(PowerUpType.SpeedDown, slowBallPanel));
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = paddle.points.ToString("0000");

        if (lives == 0)
        {
            int oldPoints = PlayerPrefs.GetInt("Points");
            PlayerPrefs.SetInt("Points",paddle.points + oldPoints);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Scenes/Scene2");

        }
    }

    public void AddLife()
    {
        lives++;
        livesText.text = "Life Left: " + lives;
    }

    public void BallLost(GameObject ball)
    {
        //Count the balls
        var balls = GameObject.FindGameObjectsWithTag("Ball");

        if (balls.Length <= 1)
        {
            LooseALife();
        }
        else
        {
            Destroy(ball);
        }
    }

    private void LooseALife()
    {
        lives--;
        livesText.text = "Life Left: " + lives;
        // paddle.GetComponent<MeshRenderer>().enabled = false;
        if (lives > 0)
        {
            ResetBallAndPaddle();
        }
    }

    public void ChangeUseConstantBallSpeed(bool value)
    {
        UseConstantBallSpeed = value;
    }

    public void ResetBallAndPaddle()
    {
        var ballObject = GameObject.FindGameObjectWithTag("Ball");
        var ballRb = ballObject.GetComponent<Rigidbody>();
        ballObject.GetComponent<MeshRenderer>().enabled = false;

        ChangeUseConstantBallSpeed(false);
        ballObject.transform.position = ballStartPos;
        ballRb.velocity = Vector3.zero;
        ballObject.GetComponent<BallControl>().Reset();


        ChangeUseConstantBallSpeed(true);

        paddle.transform.position = paddleStartPos;
        ballObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void CheckForEndOfGame()
    {
        if (GameObject.FindGameObjectsWithTag("Block") == null)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void SpawnNewBall()
    {
        Instantiate(ballPrefab, ballStartPos, Quaternion.identity);
    }
}