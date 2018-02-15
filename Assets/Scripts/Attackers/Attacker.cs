using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {

    #region Settings
    public float moveSpeed;
    public int currentWaypoint = 0;

    public float pollutionProduction;
    public float pollutionMultiplier;

    public float waypointStoppingSpeed;

    public AnimationCurve spawnChance;

    public GameObject prefab;
    #endregion

    #region References
    private Rigidbody2D rb;
    private AttackerHealth health;
    #endregion

    #region Movement
    public virtual void UpdateWaypoint() {
        Vector3 waypoint = TrackManager.instance.GetWaypoint(currentWaypoint).position;

        if (Vector2.Distance(waypoint, transform.position) <= waypointStoppingSpeed) {
            if (TrackManager.instance.WaypointExists(currentWaypoint + 1))
            {
                //No waypoints left
                rb.velocity = Vector2.zero;
                Debug.Log("Reached last waypoint.");
            }
            else {
                Debug.Log("Updating waypoint!");
                currentWaypoint++;
            }
        }
    }

    public virtual void UpdateMovement() {
        Vector2 waypoint = TrackManager.instance.GetWaypoint(currentWaypoint).position;
        Vector2 direction = waypoint - (Vector2)transform.position;
        rb.velocity = direction.normalized * moveSpeed;
    }
    #endregion

    public virtual void UpdateAttacker()
    {
        UpdateWaypoint();
        UpdateMovement();
    }

    #region Unity Functions
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<AttackerHealth>();
    }

    private void FixedUpdate()
    {
        UpdateAttacker();
    }

    private void OnCollisionEnter(Collision col)
    {
        //TODO
    }
    #endregion

}