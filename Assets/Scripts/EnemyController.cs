using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public int health = 10;
    bool carrying = false;
    Transform pathContainer;
    BowlController candyBowl;
    public AudioClip deathSound;
    public AudioClip hitSound;
    AudioSource audioPlayer;
    public AudioClip endSound;
    PlacementController player;
    
    // Start is called before the first frame update
    void Start()
    {
        // Create waypoint list from path object
        pathContainer = GameObject.FindGameObjectWithTag("Path").transform;
        Vector3[] waypoints = new Vector3[pathContainer.childCount];
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = pathContainer.GetChild(i).position;
        }

        // Get bowl object
        candyBowl = GameObject.FindGameObjectWithTag("Bowl").GetComponent<BowlController>();

        audioPlayer = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();

        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlacementController>();

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

            // Set new target once target is reached
            if (transform.position == targetWaypoint) {
                // Refresh waypoint list
                waypoints = new Vector3[pathContainer.childCount];
                for (int i = 0; i < waypoints.Length; i++) {
                    waypoints[i] = pathContainer.GetChild(i).position;
                }
                if (targetWaypointIndex > waypoints.Length - 1) {
                    targetWaypointIndex = waypoints.Length - 1;
                }

                // Destroy if at the first waypoint
                if (targetWaypointIndex == 0) {
                    Destroy(gameObject);
                }
                // Remove waypoint if carrying bowl
                if (carrying) {
                    if (targetWaypointIndex != 0) {
                        Destroy(pathContainer.GetChild(targetWaypointIndex).gameObject);
                    }
                    else {
                        // Loose condition
                        print("Game over, all your candy is belong to us.");
                        candyBowl.Drop();
                        audioPlayer.PlayOneShot(endSound);
                    }
                }
                // Final target logic
                if (targetWaypointIndex == waypoints.Length - 1) {
                    // Turn around
                    movementDirection = -1;
                    
                    // Attempt to carry bowl
                    carrying = candyBowl.Carry(gameObject);
                }

                // Set next target
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

    void OnTriggerEnter2D(Collider2D triggerCollider) {
        audioPlayer.PlayOneShot(deathSound);
        Destroy(triggerCollider.gameObject);
        if (carrying) {
            candyBowl.Drop();
        }
        player.money += 5;
        Destroy(gameObject);
    }
}
