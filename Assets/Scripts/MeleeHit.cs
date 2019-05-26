﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    [SerializeField] private int hitDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Agent agent = other.GetComponent<Agent>();
        Spawner spawner = other.GetComponent<Spawner>();

        if (agent)
        {
            if (agent.faction == Agent.Faction.Enemy)
            {
                Vector3 hitDir =
                    (agent.transform.position - transform.position).normalized;
                agent.TakeHit(hitDamage, hitDir);
            }
        }

        if (spawner)
        {
            // Spawn number of enemies
            spawner.Spawn();
        }
    }
}
