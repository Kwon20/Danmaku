using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
    private float speed = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float y = Mathf.Repeat(Time.time * speed, 1);
        Vector2 Offset = new Vector2(0, y);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Offset);
	}
}
