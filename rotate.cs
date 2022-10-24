using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
    Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = new Vector3(0, 0, 200);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(pos * Time.deltaTime * 2f);
	}
}
