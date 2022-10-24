using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Text : MonoBehaviour {
    private GameObject GameManager;
    private Text text;
	// Use this for initialization
	void Start () {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        text = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        text.text = "Score : " + GameManager.GetComponent<GameManager>().GetScore();
	}
}
