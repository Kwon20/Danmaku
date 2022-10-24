using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour {
    public float speed = 2.0f;
    public GameObject target;
    public float Rotate_speed = 200;
    private float BulletDelay = 5f;
    public short damage;
    private Rigidbody2D rb;   
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Enemy");
        rb = GetComponent<Rigidbody2D>();
        BulletDestroy();
    }
	
	// Update is called once per frame
	void Update () {
        //유도탄 함수
       if(target==null)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            Vector2 direction = (Vector2)target.transform.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * Rotate_speed;
            rb.velocity = transform.up * speed;
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(BulletDelay);
        Destroy(this.gameObject);

    }

}
