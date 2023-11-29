using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float ballVelocity = 5f;
    [SerializeField] private bool allowContinuousMovement = true;
    
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
        gameObject.GetComponent<AudioSource>().Play();
    }
}
