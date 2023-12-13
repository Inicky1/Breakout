using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BallControl : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioMixerGroup explosionMixerGroup;
    [SerializeField] private InputContainer input;
    public float ConstantBallSpeed { get; set; } = 30f;

    public bool ExplodeOnCollision { get; set; }

    private GameController _gameController;
    private Rigidbody _rigidbody;
    private Action<InputAction.CallbackContext> _subscribe;

    private void Start()
    {
        _subscribe = _ => InitialDownPush();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        _rigidbody.useGravity = false;

        input.Current.InGame.Shoot.performed += _subscribe;
    }

    private void OnDestroy()
    {
        input.Current.InGame.Shoot.performed -= _subscribe;
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
        //Invoke(nameof(InitialDownPush), 2f);
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            if (ExplodeOnCollision)
            {
                ExplodeOnCollision = false;
                Explode();
            } else return;
        }

        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }


    public void InitialDownPush()
    {
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), -1f).normalized * 5f, ForceMode.Impulse);
    }

    private void Explode()
    {
        gameObject.transform.localScale = new Vector3(10f, 10f, 10f);

        //Adds and removes a life to the player
        _gameController.AddLife();
        Invoke(nameof(ResetScale), 0.1f);

        //Play the explosion sound
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = explosionClip;
        source.outputAudioMixerGroup = explosionMixerGroup;

        source.Play();
        Destroy(source, explosionClip.length + 0.1f);
    }

    public void ResetScale()
    {
        var o = gameObject;
        o.GetComponent<MeshRenderer>().enabled = false;
        o.transform.localScale = new Vector3(1f, 1f, 1f);
        o.GetComponent<MeshRenderer>().enabled = true;
    }
}
