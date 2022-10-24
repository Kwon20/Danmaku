using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour {
    public int EnemyHp;
    public int EnemyExp;
    public Transform[] firePos;
    public GameObject eBullet;
    private bool bShoot;
    public short type;
    // Use this for initialization
    void Start() {
        bShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bShoot)
        {
            StartCoroutine(FireCycleControl());
            if (type == 1)
                Instantiate(eBullet, firePos[0].position, Quaternion.Euler(0, 0, 180));
            else if (type == 2)
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(eBullet, firePos[i].position, Quaternion.Euler(0, 0, 180));
                }
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
                GameObject.Find("Player").GetComponent<PlayerCtrl>().Expup(EnemyExp);
            }
        }
    }
    IEnumerator FireCycleControl()
    {
        bShoot = false;
        yield return new WaitForSeconds(0.1f);
        bShoot = true;
    }
}
