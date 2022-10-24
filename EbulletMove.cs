using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbulletMove : MonoBehaviour {
    private int speed = 4;
    public int damage = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
	}
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        Destroy(this.gameObject);
    }
}
