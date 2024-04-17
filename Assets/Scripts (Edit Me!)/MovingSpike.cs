using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviourDevNote
{
    /* Use the variables here to draw tool UI */
    public List<Transform> Waypoints;
    public float MoveSpeed;
    public SphereCollider hurtbox;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // DRAW YOUR TOOL UI HERE!
    }

    private void OnDrawGizmosSelected()
    {
        // DRAW YOUR ON SELECTION TOOL UI HERE!
    }

#endif
    
    /* You can ignore everything below this line for the workshop */
    #region Moving Spike Behavior

    private int _currentWaypointIndex;

    private void Start()
    {
        // start at the first waypoint
        _currentWaypointIndex = 0;
        
        transform.position = Waypoints[_currentWaypointIndex].position;
    }

    private void Update()
    {
        if (Waypoints.Count == 0)
        {
            return;
        }
        
        var target = Waypoints[_currentWaypointIndex];
        var direction = target.position - transform.position;
        var distance = MoveSpeed * Time.deltaTime;

        if (direction.magnitude <= distance)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= Waypoints.Count)
            {
                _currentWaypointIndex = 0;
            }
            
            transform.position = target.position;
        }
        else
        {
            transform.position += direction.normalized * distance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.gameObject.GetComponent<PlayerEntity>();
        if (entity != null)
        {
            entity.Hit();
        }
    }

    #endregion
}
