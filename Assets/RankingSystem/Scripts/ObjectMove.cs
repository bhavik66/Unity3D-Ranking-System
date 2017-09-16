using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float timeToReachTarget = .05f;

    int activeWaypointIndex = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, timeToReachTarget * Time.deltaTime);
    }
}
