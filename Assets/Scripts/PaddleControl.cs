using System;
using System.Collections;
using PowerUp;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class PaddleControl : MonoBehaviour
{
    //serialized fields
    
    [SerializeField] private float paddleSpeed = 10f;
    [SerializeField] private float reflectingForce = 1f;
    [SerializeField] private float maxReflectionAngleDeg = 75f;
    [SerializeField] private Rigidbody ballRb;
    [SerializeField] private InputContainer input;
    [SerializeField] private GameController gameController;

    [SerializeField] private PlayableDirector player;

    public ParticleSystem sparks;

    private Rigidbody _paddle;
    public int points=0;
    private Material _material;
    private MeshRenderer _mesh;
    private Action<InputAction.CallbackContext> _subscribe;

    public int A { get; private set; } = 0;
    public int B { get; private set; } = 0;
    public TMP_Text equation;


    // Start is called before the first frame update
    void Start()
    {
        
        _subscribe = context => OnMove(context.ReadValue<Vector2>());
        updateEquation();
        _paddle = GetComponent<Rigidbody>();
        _mesh = GetComponentInChildren<MeshRenderer>();
        _material = _mesh.material;
        input.Current.InGame.Move.performed += _subscribe;
    }

    private void OnDestroy()
    {
        input.Current.InGame.Move.performed -= _subscribe;
    }

    private void OnMove(Vector2 input)
    {
        _paddle.velocity = new Vector3(input.x * paddleSpeed, 0, 0);
    }

    private void FixedUpdate()
    {
        UpdateEquationPosition();
    }

    private void UpdateEquationPosition()
    {
        // GameObject.FindGameObjectWithTag("Equation").GetComponent(RectTransform).transform.position.x

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Number"))
        {
            var answer = A + B;
            if (collision.gameObject.GetComponent<TextMeshPro>().text == answer.ToString())
            {
                updateEquation();
                collision.gameObject.GetComponent<TextMeshPro>().color = Color.green;
                StartCoroutine(changeColor(true));
                points++;
                
                Vector3 pos = gameObject.transform.position;
                Instantiate(sparks, pos, Quaternion.identity);

            }
            else
            {
                collision.gameObject.GetComponent<TextMeshPro>().color = Color.red;
                StartCoroutine(changeColor(false));

            }
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(-20f, 20f), 20f, 30f, ForceMode.Impulse);
            collision.gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(-20f, 20f), 20f, 30f, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            
            player.Play();

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
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            var powerUp = collision.gameObject.GetComponent<IPowerUp>();
            var cScale = gameObject.transform.localScale;

            print("Power up: " + powerUp.Type);

            switch (powerUp.Type)
            {
                case PowerUpType.ExpandPaddle:
                    gameObject.transform.localScale = new Vector3( cScale.x + 0.1f, 1, 1);
                    break;
                case PowerUpType.ShrinkPaddle:
                    gameObject.transform.localScale = new Vector3( cScale.x - 0.1f, 1, 1);
                    break;
                case PowerUpType.SpeedUp:
                    foreach (var b in GameObject.FindGameObjectsWithTag("Ball"))
                    {
                        b.GetComponent<BallControl>().ConstantBallSpeed += 10f;
                    }
                    break;
                case PowerUpType.SpeedDown:
                    foreach (var b in GameObject.FindGameObjectsWithTag("Ball"))
                    {
                        b.GetComponent<BallControl>().ConstantBallSpeed -= 10f;
                    }
                    break;
                case PowerUpType.OneUp:
                    gameController.AddLife();
                    break;
                case PowerUpType.Explotion:
                    foreach (var o in GameObject.FindGameObjectsWithTag("Ball"))
                    {
                        o.GetComponent<BallControl>().ExplodeOnCollision = true;
                    }
                    break;
                case PowerUpType.Magnet:
                    foreach (var ball in GameObject.FindGameObjectsWithTag("Ball"))
                    {
                        var rb = ball.GetComponent<Rigidbody>();
                        rb.useGravity = false;
                        rb.velocity = Vector3.zero;
                    }
                    break;
                default:
                    print("TODO: Implement power up");
                    break;
            }

            Destroy(collision.gameObject);
        }
    }

    private IEnumerator changeColor(bool correct)
    {
        if (correct)
        {
            _material.color = Color.green;
        }
        else
        {
            _material.color = Color.red;
        }

        yield return new WaitForSeconds(1);

        _material.color = Color.magenta;

    }


    private void updateEquation()
    {
        A = Random.Range(1, 50);
        B = Random.Range(0, 50);
        equation.SetText( A + " + " + B + " = ?");
    }


}
