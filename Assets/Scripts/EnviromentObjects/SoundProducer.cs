using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class SoundProducer : Spawner
{
    [Header("Sound Producer")]
    [SerializeField] private float magnitudeOfShake = 2;
    [SerializeField] private float durationOfShake;

    public override void Spawn()
    {
        CameraShaker.Instance.ShakeOnce
            (magnitudeOfShake, 2.0f, durationOfShake / 2, durationOfShake / 2);
        FindObjectOfType<RippleEffect>().Emit
            (Camera.main.WorldToViewportPoint(transform.position));

        for (int i = 0; i < amountToSpawn; i++)
            base.Spawn();
    }
}
