using UnityEngine;
using UnityEngine.Playables;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float ballVelocity = 5f;
    [SerializeField] private bool allowContinuousMovement = true;

    [SerializeField] private PlayableDirector player;
    // Start is called before the first frame update
    void Start()
    {
        //var ball = GetComponent<Rigidbody>();
        //ball.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Block")) return;
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        player.Play();
    }
}
