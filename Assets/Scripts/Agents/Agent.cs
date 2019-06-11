using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Agent : MonoBehaviour
{
    public enum Faction { Player, Enemy};

    [Header("Agent")]

    [SerializeField] protected float moveSpeed = 4000.0f;
    [SerializeField] protected int   maxHP;
    [SerializeField] protected float invulnDuration;
    public Faction faction;

    

    protected Rigidbody2D    rb;
    protected SpriteRenderer sprite;
    protected int            currentHP;
    protected float          invulnTimer;

    protected bool IsInvulnerable
    {
        get
        {
            if (invulnTimer > 0.0f) return true;
            return false;
        }
        set
        {
            if (value)
            {
                invulnTimer = invulnDuration;
            }
            else
                invulnTimer = 0.0f;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        currentHP = maxHP;
    }

    protected virtual void Update()
    {
        if (invulnTimer > 0.0f)
        {
            invulnTimer -= Time.deltaTime;

            sprite.enabled =
                (Mathf.FloorToInt(invulnTimer * 20.0f) % 2) == 0;

            if (invulnTimer <= 0.0f)
            {
                sprite.enabled = true;
            }
        }        
    }

    public virtual void TakeHit(int nDamage, Vector2 hitDirection)
    {
        if (IsInvulnerable) return;

        currentHP -= nDamage;

        if (currentHP < 0)
        {
            OnDie();
        }
        else
        {
            IsInvulnerable = true;
            OnHit(hitDirection);
        }
    }

    protected virtual void OnHit(Vector2 hitDirection)
    {

    }

    public virtual void RecoverHP(int amountRecovered)
    {
        if (currentHP < maxHP) currentHP += amountRecovered;
        if (currentHP > maxHP) currentHP = maxHP;
    }

    protected virtual void OnDie()
    {
        Destroy(gameObject);
    }
}
