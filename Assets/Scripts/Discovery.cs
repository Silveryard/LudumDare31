using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Discovery : MonoBehaviour {
    public Text End;
    public Button BEnd;
    public Text Disc;
    public Text Out;

    public static Discovery Instance;

    public float Discovered;
    public float DownRate;

    protected void Awake() {
        Instance = this;
    }

    protected void Update() {
        Discovered -= DownRate * Time.deltaTime;
        if (Discovered < 0) Discovered = 0;

        if (Discovered > 100){
            Discovered = 100;
            End.gameObject.active = true;
            BEnd.gameObject.active = true;

            Destroy(GameObject.FindWithTag("Player"));
        }

        if (Discovered > 0) {
            Color discColor = Disc.color;
            discColor.a = 1;
            Disc.color = discColor;
            Color outColor = Out.color;
            outColor.a = 1;
            Out.color = outColor;
            Out.text = Discovered.ToString("0") + "%";
        } else {
            Color discColor = Disc.color;
            discColor.a = 0;
            Disc.color = discColor;
            Color outColor = Out.color;
            outColor.a = 0;
            Out.color = outColor;
        }
    }

    public void Replay() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
