using UnityEngine;
using Random = UnityEngine.Random;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float ballVelocity = 5f;
    [SerializeField] private bool allowContinuousMovement = true;
    public float ConstantBallSpeed { get; set; } = 30f;


    private GameController _gameController;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnBecameInvisible()
    {
        _gameController.BallLost(gameObject);
    }

    private void FixedUpdate()
    {
        if (_gameController.UseConstantBallSpeed)
        {
            _rigidbody.velocity = ConstantBallSpeed * (_rigidbody.velocity.normalized);
        }
    }

    public void Reset()
    {
        _rigidbody.useGravity = false;
        Invoke(nameof(InitialDownPush), 2f);
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Block")) return;
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }


    public void InitialDownPush()
    {
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), -1f).normalized * 5f, ForceMode.Impulse);
    }
}
