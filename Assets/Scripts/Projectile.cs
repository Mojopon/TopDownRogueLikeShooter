using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    private float speed = 10;

	public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
	
	void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
