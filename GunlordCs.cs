using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunlordCs : MonoBehaviour
{

    public int EnemyHp;
    public int EnemyExp;
    public Transform firePos;
    public GameObject eBullet;
    private bool bShoot;

    // Use this for initialization
    void Start()
    {      
        StartCoroutine(FireCycleControl());
    }

    // Update is called once per frame
    void Update()
    {
 

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
                GameObject.Find("Player").GetComponent<PlayerCtrl>().Expup(EnemyExp);
            }
        }
    }
    IEnumerator FireCycleControl()
    {
        bShoot = false;
        yield return new WaitForSeconds(1.5f);
        bShoot = true;
    }
}
