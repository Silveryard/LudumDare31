using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public EnemyManager Manager;
    public Waypoint NextTarget;

    public float RotationSpeed;
    public float Speed;

    //0 = rotating
    //1 = moving
    private int _mode = 0;

    protected void Update() {
        if (_mode == 0) {
            Vector3 direction = (NextTarget.transform.position - transform.position).normalized;
            Quaternion q = Quaternion.LookRotation(direction);
            float before = transform.rotation.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);
            float after = transform.rotation.y;

            float delta = before - after;
            if (delta < 0) delta *= -1;
            if (delta < 0.0005f)
                _mode = 1;
        } else {
            transform.position = Vector3.MoveTowards(transform.position, NextTarget.transform.position, Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, NextTarget.transform.position) < 0.1) {
                Waypoint target = NextTarget.Neighbours[Random.Range(0, NextTarget.Neighbours.Count)];
                MoveToNextTarget(target);
            }
        }
    }

    public void MoveToNextTarget(Waypoint nextTarget) {
        _mode = 0;
        NextTarget = nextTarget;
    }

}
