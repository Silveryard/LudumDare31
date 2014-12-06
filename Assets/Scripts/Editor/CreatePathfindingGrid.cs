using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreatePathfindingGrid : MonoBehaviour {

    [MenuItem("LD31/CreatePathfindingGrid")]
    public static void CreateGrid() {
        GameObject manager = GameObject.FindWithTag("GameController");

        ResetGrid(manager);
        CreateGrid(manager);
    }

    private static void ResetGrid(GameObject manager) {
        foreach (Transform child in manager.transform) {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            waypoint.Neighbours = new System.Collections.Generic.List<Waypoint>();
        }
    }

    private static void CreateGrid(GameObject manager) {
        foreach (Transform child in manager.transform) {
            Waypoint waypointChild = child.GetComponent<Waypoint>();
            foreach (Transform neighbour in manager.transform) {
                Waypoint waypointNeighbour = neighbour.GetComponent<Waypoint>();

                if (neighbour == child) continue;

                var pos1 = child.transform.position;
                var pos2 = neighbour.transform.position;
                var delta = Vector3.Distance(pos1, pos2);

                if (delta >= 10) continue;

                RaycastHit[] hits;
                hits = Physics.RaycastAll(pos1, Vector3.Normalize(pos2 - pos1), delta);

                bool isWayFree = true;

                foreach (RaycastHit hit in hits) {
                    if (!hit.collider.isTrigger) {
                        isWayFree = false;
                        break;
                    }
                }

                if (isWayFree) {
                    waypointChild.Neighbours.Add(waypointNeighbour);
                }
            }
        }
    }

}
