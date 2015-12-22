using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    Vector3 velocity;
    Rigidbody2D myRigidbody;
	
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

	public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void LookAt(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
 
    void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + (Vector2)velocity * Time.fixedDeltaTime);
    }
}
