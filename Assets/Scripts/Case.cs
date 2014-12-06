using UnityEngine;
using System.Collections.Generic;

public class Case : MonoBehaviour {

    public CaseManager Manager;

    public GameObject Main;
    public GameObject HandUpper;
    public GameObject HandLeft;
    public GameObject HandRight;

    public Material TextureMainUnlit;
    public Material TextureMainLit;
    public Material TextureHandUnlit;
    public Material TextureHandLit;

    protected void Update() {
        RaycastHit[] hits;
        GameObject player = GameObject.FindWithTag("Player");

        hits = Physics.RaycastAll(transform.position, Vector3.Normalize(player.transform.position - transform.position), Vector3.Distance(transform.position, player.transform.position));

        bool wayFree = true;
        foreach(RaycastHit hit in hits){
            if(hit.collider.tag != "Player")
                wayFree = false;
        }

        float angle = Vector3.Angle(player.transform.forward, transform.position - player.transform.position);
        if (angle < 0) angle *= -1;

        if (angle > 50) wayFree = false;

        if (wayFree) {
            Main.renderer.material = TextureMainLit;
            HandUpper.renderer.material = TextureHandLit;
            HandLeft.renderer.material = TextureHandLit;
            HandRight.renderer.material = TextureHandLit;
        } else {
            Main.renderer.material = TextureMainUnlit;
            HandUpper.renderer.material = TextureHandUnlit;
            HandLeft.renderer.material = TextureHandUnlit;
            HandRight.renderer.material = TextureHandUnlit;
        }
    }

    protected void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Manager.SpawnNewCase();
            Manager.Cases++;
            Destroy(gameObject);
        }
    }
	
}
