using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaschyteCs : MonoBehaviour {

    private int EnemyHp = 40;
    private int EnemyExp = 15;
    private int E_score = 400;
    public Transform firePos;
    public GameObject eBullet;
    public GameObject Explosion;
    private bool bShoot;
    private float speed = 1.5f;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(FireCycleControl());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (bShoot)
        {
            StartCoroutine(FireCycleControl());
            Instantiate(eBullet, firePos.position, Quaternion.Euler(0, 0, 180));
        }
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
    IEnumerator FireCycleControl()
    {
        bShoot = false;
        yield return new WaitForSeconds(1.5f);
        bShoot = true;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
