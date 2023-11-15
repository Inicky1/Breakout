using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float ballVelocity = 5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        var ball = GetComponent<Rigidbody>();
        ball.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position += transform.up * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision other)
    {
        // Quaternion newRotation = this.transform.rotation;
        // newRotation.z = -newRotation.z;
        // transform.rotation = newRotation;
    }
}
