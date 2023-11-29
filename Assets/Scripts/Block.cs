using UnityEngine;
using UnityEngine.Audio;

public class Block : MonoBehaviour
{

    [SerializeField]
    private AudioClip explosionClip;
    [SerializeField]
    private AudioMixerGroup mixerGroup;

    private void OnCollisionEnter(Collision c)
    {
        if (!c.gameObject.CompareTag("Ball")) return;

        gameObject.SetActive(false);
        //Play the explosion sound effect at the mixerGroup
        var source = c.gameObject.AddComponent<AudioSource>();

        source.clip = explosionClip;
        source.outputAudioMixerGroup = mixerGroup;
        source.Play();

        Destroy(source, explosionClip.length + 0.1f);
    }
}
