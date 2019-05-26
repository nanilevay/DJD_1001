using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProducer : Spawner
{
    [Header("Alarm")]
    [SerializeField] ParticleSystem soundWaveFx;
    [SerializeField] private int    amountToSpawn;

    private void Awake()
    {
        soundWaveFx.Stop();
    }

    public override void Spawn()
    {
        for (int i = 0; i < amountToSpawn; i++)
            base.Spawn();

        soundWaveFx.Play();
    }
}
