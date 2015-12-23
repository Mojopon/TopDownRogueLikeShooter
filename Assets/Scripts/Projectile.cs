using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    private float speed = 10;
    private float damage = 1;

	public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
	
	void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if(damageableObject != null)
        {
            damageableObject.TakeHit(damage);
        }

        Destroy(gameObject);
    }
}
