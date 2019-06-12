using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] private bool isInstantKill;
    [SerializeField] private int  damageOnTouch;
    [SerializeField] private bool isTurnedOn;

    protected Collider2D damageBody;
    protected Collider2D other;

    protected virtual void Start()
    {
        damageBody = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        if (isTurnedOn)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.ClearLayerMask();
            contactFilter.SetLayerMask(LayerMask.GetMask("Agent"));

            Collider2D[] results = new Collider2D[15];

            int nCols = Physics2D.OverlapCollider
                (damageBody, contactFilter, results);

            if (nCols > 0)
            {
                for (int i = 0; i < nCols; i++)
                {
                    other = results[i];

                    Agent agent = other.GetComponent<Agent>();
                    if (agent)
                    {
                        Vector2 hitDir = (agent.transform.position - transform.position)
                            .normalized;
                        hitDir.y = 1.0f;

                        if (isInstantKill)
                        {
                            agent.TakeHit(4, hitDir);
                        }
                        else
                        {
                            agent.TakeHit(damageOnTouch, hitDir);

                        }
                    }
                }
            }
        }
    }
}
