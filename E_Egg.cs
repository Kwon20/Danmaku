using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Egg : MonoBehaviour {
    public GameObject[] Mob;
    int x;
    private int EnemyHp = 200;
    private int EnemyExp = 30;
	// Use this for initialization
	void Start () {
        float y = Random.Range(0, 3f);
        if (y < 1)
            x = 0;
        else if (y < 2)
            x = 1;
        else
            x = 2;
        StartCoroutine(ChangeMob());
	}
	
	// Update is called once per frame
	void Update () {
		
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
                GameObject.Find("Player").GetComponent<PlayerCtrl>().Expup(EnemyExp);
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
                GameObject.Find("Player").GetComponent<PlayerCtrl>().Expup(EnemyExp);
            }
        }
    }
    IEnumerator ChangeMob()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(Mob[x], this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
