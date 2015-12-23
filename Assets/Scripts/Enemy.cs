using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float turnSpeed = 120f;

    private Transform target;
    private Rigidbody2D myRigidbody;
    private float currentAngle;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        var angle = RotationHelper.GetAngleFromToTarget(transform.position, target.position);

        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var angle = RotationHelper.RotateToTarget(transform.position, target.position, currentAngle, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        currentAngle = angle;
    }

    void FixedUpdate()
    {
        var directionToTarget = (target.position - transform.position).normalized;
        var moveVelocity = directionToTarget * Time.deltaTime;

        myRigidbody.MovePosition(transform.position + moveVelocity);
    }
}
