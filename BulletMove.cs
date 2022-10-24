using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    private float speed = 10.0f;
    public short damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
