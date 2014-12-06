using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

    public List<Waypoint> Neighbours;

    protected void OnDrawGizmos() {
        Color c = new Color(0, 1, 0, 1);

        if (Neighbours.Count == 0)
            c = new Color(1, 0, 0, 1);

        Gizmos.color = c;
        Gizmos.DrawCube(this.transform.position, new Vector3(1.2f, 3f, 1.2f));
    }

    protected void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);

        foreach (Waypoint neighbour in Neighbours) {
            Gizmos.DrawLine(transform.position, neighbour.transform.position);
        }
    }
}
