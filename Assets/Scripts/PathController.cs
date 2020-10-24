using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    // Display the path in editor for ease of use
    void OnDrawGizmos() {
        Vector3 startPosition = transform.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach(Transform waypoint in transform) {
            Gizmos.DrawSphere(waypoint.position, .5f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
    }
}