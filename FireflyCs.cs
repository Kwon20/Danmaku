using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyCs : MonoBehaviour {

    private int EnemyHp = 30;
    private int EnemyExp = 10;
    private int E_score = 300;
    public Transform firePos;
    public GameObject eBullet;
    public GameObject Explosion;
    private bool bShoot;
    private bool back;
    private float speed = 3.0f;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(FireCycleControl());
        back = false;
        Invoke("Isback", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (bShoot)
        {
            StartCoroutine(FireCycleControl());
            Instantiate(eBullet, firePos.position, Quaternion.Euler(0, 0, 180));
        }
        if(back)
            transform.Translate(Vector2.up * speed/2 * Time.deltaTime);
        if(back == false)
            transform.Translate(Vector2.down * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            EnemyHp -= other.GetComponent<BulletMove>().damage;
            Destroy(other.gameObject);
            if (EnemyHp <= 0)
            {
                Destroy(gameObject);
                this.gameObject.GetComponent<DropItem>().ItemDrop();
                GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>().Expup(EnemyExp);
                GameObject.Find("GameManager").GetComponent<GameManager>().ScoreUp(E_score);
                Instantiate(Explosion, firePos.position, Quaternion.identity);
            }
        }
        if (other.tag == "Missile")
        {
            EnemyHp -= other.GetComponent<MissileMove>().damage;
            Destroy(other.gameObject);
            if (EnemyHp <= 0)
            {
                Destroy(gameObject);
                this.gameObject.GetComponent<DropItem>().ItemDrop();
                GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>().Expup(EnemyExp);
                GameObject.Find("GameManager").GetComponent<GameManager>().ScoreUp(E_score);
                Instantiate(Explosion, firePos.position, Quaternion.identity);
            }
        }
    }
    private void Isback()
    {
        back = true;
    }
    IEnumerator FireCycleControl()
    {
        bShoot = false;
        yield return new WaitForSeconds(1.0f);
        bShoot = true;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}