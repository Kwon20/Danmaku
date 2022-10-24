using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Time_Text : MonoBehaviour {
    private Text text;
    public GameObject Gm;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        Gm = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update() {
        Gm = GameObject.FindGameObjectWithTag("GameManager");
        text.text = "Time : " + Gm.GetComponent<GameManager>().GetTime().ToString("N2");
    }
}
