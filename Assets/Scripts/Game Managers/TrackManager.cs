using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour {

    #region Singleton
    public static TrackManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform[] waypoints;

    public Transform GetWaypoint(int waypoint) {
        return waypoints[waypoint];
    }

    public bool WaypointExists(int index) {
        return waypoints.Length - 1 < index;
    }

}