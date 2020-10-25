using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public int health = 10;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Create waypoint list from path object
        Transform pathContainer = GameObject.FindGameObjectWithTag("Path").transform;
        Vector3[] waypoints = new Vector3[pathContainer.childCount];
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = pathContainer.GetChild(i).position;
        }

        // Start following the path
        StartCoroutine(FollowPath(waypoints));
    }

    // Path following coroutine
    IEnumerator FollowPath(Vector3[] waypoints) {
        // Move to first waypoint and set up targets
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        LookAt2D(targetWaypoint);
        int movementDirection = 1;

        // Movement loop
        while (true) {
            // Move towards target waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

            // Determine next target once target is reached
            if (transform.position == targetWaypoint) {
                if (targetWaypointIndex == waypoints.Length - 1) {
                    movementDirection = -1;
                }
                targetWaypointIndex += movementDirection;
                targetWaypoint = waypoints[targetWaypointIndex];
                LookAt2D(targetWaypoint);
            }
            yield return null;
        }
    }

    // 2D Lookat
    void LookAt2D(Vector3 targetPoint) {
        targetPoint.x -= transform.position.x;
        targetPoint.y -= transform.position.y;
        float angle = Mathf.Atan2(targetPoint.y, targetPoint.x) * Mathf.Rad2Deg;
        angle -= 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
