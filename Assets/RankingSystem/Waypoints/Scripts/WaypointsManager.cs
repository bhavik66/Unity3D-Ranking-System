using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointsManager : MonoBehaviour
{
    public static WaypointsManager instance;

    public List<Transform> waypoints = new List<Transform>();

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        ResetWaypoints();
    }

    void ResetWaypoints()
    {
        waypoints.Clear();

        foreach (Transform waypoint in GetComponentsInChildren<Transform>())
        {
            if (waypoint != transform)
            {
                waypoints.Add(waypoint);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
