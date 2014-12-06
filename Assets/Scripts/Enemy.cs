using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public List<GameObject> Body;
    public List<GameObject> Arms;
    public GameObject Nose;
    public List<GameObject> Eyes;

    public Material MaterialBodyLit;
    public Material MaterialBodyUnlit;
    public Material MaterialArmsLit;
    public Material MaterialArmsUnlit;
    public Material MaterialNoseLit;
    public Material MaterialNoseUnlit;
    public Material MaterialEyesLit;
    public Material MaterialEyesUnlit;

    public EnemyManager Manager;
    public Waypoint NextTarget;

    public float Chase;
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
        CheckLightning();
        CheckLooking();
    }

    private void CheckLightning() {
        RaycastHit[] hits;
        GameObject player = GameObject.FindWithTag("Player");

        hits = Physics.RaycastAll(transform.position, Vector3.Normalize(player.transform.position - transform.position), Vector3.Distance(transform.position, player.transform.position));

        bool wayFree = true;
        foreach (RaycastHit hit in hits) {
            if (hit.collider.tag != "Player")
                wayFree = false;
        }

        float angle = Vector3.Angle(player.transform.forward, transform.position - player.transform.position);
        if (angle < 0) angle *= -1;

        if (angle > 50) wayFree = false;

        Material materialBody;
        Material materialArms;
        Material materialEyes;
        Material materialNose;

        if (wayFree) {
            materialBody = MaterialBodyLit;
            materialArms = MaterialArmsLit;
            materialEyes = MaterialEyesLit;
            materialNose = MaterialNoseLit;
        } else {
            materialBody = MaterialBodyUnlit;
            materialArms = MaterialArmsUnlit;
            materialEyes = MaterialEyesUnlit;
            materialNose = MaterialNoseUnlit;
        }

        foreach (GameObject body in Body) {
            body.renderer.material = materialBody;
        }
        foreach (GameObject arm in Arms) {
            arm.renderer.material = materialArms;
        }
        foreach (GameObject eye in Eyes) {
            eye.renderer.material = materialEyes;
        }
        Nose.renderer.material = materialNose;
    }

    private void CheckLooking() {
        RaycastHit[] hits;
        GameObject player = GameObject.FindWithTag("Player");

        hits = Physics.RaycastAll(transform.position, Vector3.Normalize(player.transform.position - transform.position), Vector3.Distance(transform.position, player.transform.position));

        bool wayFree = true;
        foreach (RaycastHit hit in hits) {
            if (hit.collider.tag != "Player")
                wayFree = false;
        }

        float angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
        if (angle < 0) angle *= -1;

        if (angle > 40) wayFree = false;

        if (wayFree) {
            Discovery.Instance.Discovered += Chase * Time.deltaTime;
        }
    }

    public void MoveToNextTarget(Waypoint nextTarget) {
        _mode = 0;
        NextTarget = nextTarget;
    }

}
