using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public GameObject EnemyPrefab;

    public List<Waypoint> Waypoints;

    protected void Awake() {
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
    }

    public void SpawnEnemy() {
        Waypoint waypoint = Waypoints[Random.Range(0, Waypoints.Count)];
        GameObject enemy = (GameObject)Instantiate(EnemyPrefab, waypoint.transform.position, Quaternion.identity);

        Enemy eScript = enemy.GetComponent<Enemy>();

        Waypoint target = waypoint.Neighbours[Random.Range(0, waypoint.Neighbours.Count)];
        eScript.Manager = this;
        eScript.MoveToNextTarget(target);
    }
	
}
