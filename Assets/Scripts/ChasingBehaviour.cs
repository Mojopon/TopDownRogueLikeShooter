using UnityEngine;
using System.Collections;

public class ChasingBehaviour : MonoBehaviour
{
    public Transform target;
    public bool isDead = false;

    Rigidbody2D myRigidbody;
    float speed = 3;
    Vector3[] path;
    int targetIndex;

    float pathUpdateTime = 0.25f;
    float lastUpdated;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Time.time > lastUpdated)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            lastUpdated = Time.time + pathUpdateTime;
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(!isDead && pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    
    IEnumerator FollowPath()
    {
        if (path.Length == 0) yield break;
        Vector3 currentWayPoint = path[0];
        targetIndex = 0;

        while(true)
        {
            if(transform.position == currentWayPoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }

            myRigidbody.MovePosition(Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime));
            yield return null;
        }
    }
}
