﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : LivingEntity
{
    public float moveSpeed = 3f;
    public float turnSpeed = 120f;

    private Transform target;
    private Rigidbody2D myRigidbody;
    private float currentAngle;

    protected override void Start()
    {
        base.Start();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Die()
    {
        base.Die();
        GetComponent<ChasingBehaviour>().isDead = true;
    }

    void Update()
    {
        var angle = RotationHelper.RotateToTarget(transform.position, target.position, currentAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        currentAngle = angle;
    }
}
