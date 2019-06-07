using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProducer : Spawner
{
    public override void Spawn()
    {
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        for (int i = 0; i < amountToSpawn; i++)
            base.Spawn();

    }
}
