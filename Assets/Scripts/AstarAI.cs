using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
using UniRx;
public class AstarAI : MonoBehaviour
{
    //The point to move to
    public Vector3 targetPosition;
    private Transform target;
    private Seeker seeker;
    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed = 100;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    public LayerMask playerMask;
    public float raycastFrequency = 2;
    private float lastRaycastTime;
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    private bool foundPlayer = false;
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        Pathfind();
    }

    void Pathfind()
    {
        var coroutine = Observable.FromCoroutine<Path>(observer => UpdatePath(observer));
        coroutine.Subscribe(p => 
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        });
    }

    IEnumerator UpdatePath(IObserver<Path> observer)
    {
        targetPosition = target.position;
        var path = seeker.StartPath(transform.position, targetPosition);

        observer.OnNext(path);
        observer.OnCompleted();

        yield break;
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }
    public void Update()
    {
        if(Time.time > lastRaycastTime)
        {
            foundPlayer = CheckIfPlayerIsInSight();
            lastRaycastTime += Time.time + raycastFrequency;
        }

        if(foundPlayer)
        {
            ChasePlayer();
        }
        else
        {
            TracePath();
        }
    }

    void ChasePlayer()
    {
        var dir = (target.position - transform.position).normalized;
        dir *= speed * Time.deltaTime;
        transform.Translate(dir);
    }

    void TracePath()
    {
        if (path == null)
        {
            //We have no path to move after yet
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            // reached the end node
            return;
        }
        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.deltaTime;
        //controller.SimpleMove(dir);
        transform.Translate(dir);
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    bool CheckIfPlayerIsInSight()
    {
        Vector2 dir = (target.position - transform.position).normalized;

        if (Physics2D.Raycast(transform.position, dir, 20, playerMask))
        {
            Debug.Log("Hit to player");
            return true;
        }
        else
        {
            return false;
        }
    }
}