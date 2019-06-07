using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProducer : Spawner
{
    [Header("Alarm")]
    [SerializeField] ParticleSystem soundWaveFx;
    [SerializeField] AudioSource    activationSound;
    private void Awake()
    {
        activationSound = GetComponent<AudioSource>();
        soundWaveFx.Stop();
    }

    public override void Spawn()
    {
        for (int i = 0; i < amountToSpawn; i++)
            base.Spawn();

        if (soundWaveFx) soundWaveFx.Play();
        if (activationSound) activationSound.Play();
    }
}
