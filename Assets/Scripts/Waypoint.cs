using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

    public List<Waypoint> Neighbours;

    protected void OnDrawGizmosSelected() {
        foreach (Waypoint neighbour in Neighbours) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, neighbour.transform.position);
        }
    }
}
