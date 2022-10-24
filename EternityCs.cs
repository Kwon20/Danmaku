using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternityCs : MonoBehaviour {

    private int EnemyHp = 180;
    private int EnemyExp = 50;
    private int E_score = 1300;
    public Transform firePos;
    public GameObject eBullet;
    public GameObject Explosion;
    private float speed = 1.5f;
    private bool bShoot;
    
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
            for(int i=1; i<=5; i++)             
            Instantiate(eBullet, firePos.position, Quaternion.Euler(0, 0, i*10+150));
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
        yield return new WaitForSeconds(3.0f);
        bShoot = true;
    }
}
