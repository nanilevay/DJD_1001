using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("PickUps")]
    [SerializeField] protected int valueToAdd;
    [SerializeField] GameObject pickupFx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Agent agent = other.GetComponent<Agent>();
        if (agent != null)
        {
            if (agent.faction == Agent.Faction.Player)
            {
                // Add player HP to LevelMng or GameMng.
                SendPickUp(agent);
            }

            if (pickupFx != null)
            {
                Instantiate(pickupFx, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void SendPickUp(Agent agent)
    {

    }
}
