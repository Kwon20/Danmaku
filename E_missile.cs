using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_missile : MonoBehaviour
{
    public float speed = 6.0f;
    public GameObject target;
    public float Rotate_speed = 200;
    private float BulletDelay = 5f;
    public short damage;
    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        BulletDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        //유도탄 함수
        if (target == null)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
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
        if (collision.gameObject.tag == "Player")
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