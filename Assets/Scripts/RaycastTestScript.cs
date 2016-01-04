using UnityEngine;
using System.Collections;

public class RaycastTestScript : MonoBehaviour
{
    public Transform target;
    public LayerMask playerMask;

    private Vector3 hitPoint = Vector3.zero;

    void Update()
    {
        Debug.Log("raycasting");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, GetDirectionToTarget(), 30, playerMask);
        if(hit.collider != null)
        {
            hitPoint = hit.point;
        }
    }

    void OnDrawGizmos()
    {
        if (hitPoint == Vector3.zero) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, hitPoint);
    }

    Vector3 GetDirectionToTarget()
    {
        if (target == null) return Vector3.one;

        return (target.position - transform.position).normalized;
    }
}
