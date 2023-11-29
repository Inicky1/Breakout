using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Block : MonoBehaviour
{

    [SerializeField]
    private AudioClip explosionClip;
    [SerializeField]
    private AudioClip hitClip;
    [SerializeField]
    private AudioMixerGroup explosionMixerGroup;
    [SerializeField]
    private AudioMixerGroup hitMixerGroup;
    [SerializeField]
    private int hitPoints;
    [SerializeField]
    private GameObject number;

    private void OnCollisionEnter(Collision c)
    {
        hitPoints--;
        if (hitPoints > 0)
        {
            var source = gameObject.AddComponent<AudioSource>();

            source.clip = hitClip;
            source.outputAudioMixerGroup = hitMixerGroup;
            source.Play();

            Destroy(source, hitClip.length + 0.1f);
        }
        else
        {
            gameObject.SetActive(false);
            //Play the explosion sound effect at the mixerGroup
            var source = c.gameObject.AddComponent<AudioSource>();

            source.clip = explosionClip;
            source.outputAudioMixerGroup = explosionMixerGroup;
            source.Play();

            Destroy(source, explosionClip.length + 0.1f);

                if (Random.Range(0, 100) < 75)
            {
                float x = Mathf.Ceil(Random.Range(0f, 100f));

                number.GetComponent<TMP_Text>().text = x.ToString();
                Vector3 pos = gameObject.transform.position;
                Instantiate(number, pos, Quaternion.identity);
            }
        }
    }

}
