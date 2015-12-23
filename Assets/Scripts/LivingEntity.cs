﻿using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
}
