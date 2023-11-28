using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField]
    private AudioClip explosionClip;

    private void OnCollisionEnter(Collision c)
    {
        if (!c.gameObject.CompareTag("Ball")) return;

        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(explosionClip, transform.position);
    }
}
