using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour {

    public float moveSpeed = 5;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

	void Update ()
    {
        // Move player
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        viewCamera.transform.position = new Vector3(transform.position.x, transform.position.y, viewCamera.transform.position.z);

        // Rotate player towards mouse position
        Vector2 mousePosition = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                          Input.mousePosition.y,
                                                                          -viewCamera.transform.position.z));
        float angle = RotationHelper.GetAngleFromToTarget(transform.position, mousePosition);
        controller.LookAt(angle);

        // Shoot the gun
        if(Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
