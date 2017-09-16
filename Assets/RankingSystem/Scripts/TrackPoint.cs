using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPoint : MonoBehaviour
{
    [Header("Debug")]
    public bool ShowLine = false;

    GameObject trackPoint;
    int activeWaypointIndex = 0;

    Vector3 lineStart;
    Vector3 lineEnd;

    float distanceToEndPoint;
    float distanceToStartPoint;

    float tempDistanceToEndPoint = -1f;
    float tempActiveWaypoint;


    // Use this for initialization
    void Start()
    {
        trackPoint = new GameObject();
        trackPoint.name = "TrackPoint_" + name;
        IconManager.SetIcon(trackPoint, IconManager.Icon.DiamondPurple);
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        SetActiveWaypoint();

        RankManager.instance.SetRank(new Player()
        {
            activeWaypointIndex = this.activeWaypointIndex,
            distanceToWaypoint = distanceToEndPoint,
            name = this.name
        });

        if (ShowLine)
            Debug.DrawLine(transform.position, trackPoint.transform.position, Color.magenta);
    }

    //Set tracking point position on waypoint
    private void SetPosition()
    {
        lineStart = WaypointsManager.instance.waypoints[activeWaypointIndex].position;

        lineEnd = lineStart;
        if (activeWaypointIndex < WaypointsManager.instance.waypoints.Count - 1)
            lineEnd = WaypointsManager.instance.waypoints[activeWaypointIndex + 1].position;


        Vector3 normal = (lineStart - lineEnd).normalized;
        Vector3 pos = lineStart + Vector3.Project(transform.position - lineStart, normal);

        pos.x = Mathf.Clamp(pos.x, Mathf.Min(lineStart.x, lineEnd.x), Mathf.Max(lineStart.x, lineEnd.x));
        //pos.y = Mathf.Clamp(pos.y, Mathf.Min(lineStart.y, lineEnd.y), Mathf.Max(lineStart.y, lineEnd.y));
        pos.z = Mathf.Clamp(pos.z, Mathf.Min(lineStart.z, lineEnd.z), Mathf.Max(lineStart.z, lineEnd.z));

        trackPoint.transform.position = pos;
    }


    // Set active waypoint index and distance to waypoint
    private void SetActiveWaypoint()
    {
        distanceToEndPoint = Vector3.Distance(lineEnd, trackPoint.transform.position);
        distanceToStartPoint = Vector3.Distance(lineStart, trackPoint.transform.position);

        if (0.1 >= distanceToEndPoint)
        {
            if (activeWaypointIndex < WaypointsManager.instance.waypoints.Count - 1)
            {
                tempDistanceToEndPoint = -1f;
                activeWaypointIndex++;
            }
        }
        else if (0.1 >= distanceToStartPoint)
        {
            if (activeWaypointIndex > 0)
            {
                tempDistanceToEndPoint = -1f;
                activeWaypointIndex--;
            }
        }
    }
}
