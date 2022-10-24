using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour {
	public GUIText score_te;
	public float score_ti;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		score_ti += Time.deltaTime;
		score_te.text = string.Format ("{0}", score_ti);
	}
}
