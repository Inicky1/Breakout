using System;
using System.Collections.Generic;
using System.Linq;
using PowerUp;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using Random = UnityEngine.Random;

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
    [SerializeField]
    private PaddleControl paddleControl;
    [SerializeField]
    private float chanceToSpawnNumber;
    [SerializeField]
    private float changeToSpawnRightNumber;
    [SerializeField]
    private float chanceToSpawnPowerUp;
    [SerializeField]
    private GameController gameController;

    private MeshRenderer _mesh;
    private Material _material;

    private void Start()
    {
        //Change the color of the block with the hitpoints
        _mesh = GetComponentInChildren<MeshRenderer>();
        _material = _mesh.material;
        _material.color = Color.Lerp(Color.white, Color.red, (float)hitPoints / 3);
    }

    private void OnCollisionEnter(Collision c)
    {
        hitPoints--;
        if (hitPoints > 0)
        {
            var source = gameObject.AddComponent<AudioSource>();

            source.clip = hitClip;
            source.outputAudioMixerGroup = hitMixerGroup;
            source.pitch = Random.Range(0.8f, 1.2f);
            source.Play();

            Destroy(source, hitClip.length + 0.1f);

            _material.color = Color.Lerp(Color.white, Color.red, (float)hitPoints / 3);
        }
        else
        {
            gameObject.SetActive(false);
            //Play the explosion sound effect at the mixerGroup
            var source = c.gameObject.AddComponent<AudioSource>();

            source.clip = explosionClip;
            source.outputAudioMixerGroup = explosionMixerGroup;
            source.pitch = Random.Range(0.8f, 1.2f);
            source.Play();

            var pos = source.transform.position;

            Destroy(source, explosionClip.length + 0.1f);

            if (Random.Range(0, 100) < chanceToSpawnNumber)
            {
                float x = paddleControl.A + paddleControl.B;
                //Check if it should spawn the right number
                if (Random.Range(0, 100) < changeToSpawnRightNumber)
                {
                    x += Random.Range(-10, 10);
                }

                number.GetComponent<TMP_Text>().text = x.ToString();
                Spawn(number, pos);
            }
            else if (Random.Range(0, 100) < chanceToSpawnPowerUp)
            {
                var powerUp = gameController.PowerUp
                    .Where(p => p.IsEnable)
                    .Select(p => p.GmObject)
                    .ToList();

                int i = Random.Range(0, powerUp.Count);
                var randomPowerUp = powerUp[i];

                Spawn(randomPowerUp, pos);
            }
        }
    }

    private void Spawn(GameObject gameObject, Vector3 pos)
    {
        var inst = Instantiate(gameObject, pos, Quaternion.identity);

        Destroy(inst, 10);
    }

}
