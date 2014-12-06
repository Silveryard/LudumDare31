using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CaseManager : MonoBehaviour {

    public int Cases;

    public Text Output;

    public EnemyManager Manager;
    public GameObject CasePrefab;
    public List<GameObject> Spawns;

    protected void Awake() {
        SpawnNewCase();
    }

    protected void Update() {
        Output.text = "" + Cases;
    }

    public void SpawnNewCase() {
        GameObject spawn = Spawns[Random.Range(0, Spawns.Count)];
        GameObject newCase = (GameObject)Object.Instantiate(CasePrefab, spawn.transform.position, Quaternion.identity);
        Case caseComponent = newCase.GetComponent<Case>();
        caseComponent.Manager = this;
        Manager.SpawnEnemy();
    }
	
}
