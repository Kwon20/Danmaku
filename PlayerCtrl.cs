using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float Speed = 3.5f;
    private SpriteRenderer _renderer;
    public Sprite[] P_Sprite;
    public Transform firePos;
    public Transform[] missilePos;
    public GameObject[] bullet;
    public GameObject[] missile;
    public GameObject Explosion;
    private bool bShoot;
    private bool mShoot;
    public short P_level;
    public short level;
    private int Exp;
    private float Hp;
    private GameObject GameManager;
    private bool isLaseron = false;
    private bool isInvinvible = false;
    void Start()
    {
        P_level = 1;
        Hp = 50;
        level = 1;
        bShoot = true;
        mShoot=true;
        _renderer = this.GetComponent<SpriteRenderer>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        StartCoroutine(P_Spawn());
    }
    // Update is called once per frame
    void Update()
    {
        if (isLaseron)
            Hp -= 0.2f;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // 매 프레임마다 메소드 호출
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bShoot)
            {
                StartCoroutine(FireCycleControl());
                Fire();
            }
        }
        if(mShoot)
        {
            if (GameObject.FindWithTag("Enemy") != null)
            {
                if (level == 2)
                {
                    StartCoroutine(MissileCycle());
                    Instantiate(missile[0], missilePos[0].position, Quaternion.identity);
                    Instantiate(missile[0], missilePos[1].position, Quaternion.identity);
                }
                else if (level == 3)
                {
                    StartCoroutine(MissileCycle());
                    Instantiate(missile[1], missilePos[0].position, Quaternion.identity);
                    Instantiate(missile[1], missilePos[1].position, Quaternion.identity);
                }
            }
        }
    }

    // 움직이는 기능을 하는 메소드
    private void Move(Vector2 direction)
    {
        if (direction.x > 0.2)
        {
            int x = level * 3 - 1;
            _renderer.sprite = P_Sprite[x];
        }
        else if (direction.x < -0.2)
        {
            int x = level * 3 - 3;
            _renderer.sprite = P_Sprite[x];
        }
        else
        {
            int x = level * 3 - 2;
            _renderer.sprite = P_Sprite[x];
        }
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 pos = transform.position;
        pos += direction * Speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x - 0.5f);
        pos.y = Mathf.Clamp(pos.y, min.y + 0.5f, max.y - 0.5f);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "E_Bullet")
        {
            if (!isInvinvible)
            {
                Hp -= collision.GetComponent<EbulletMove>().damage;
                if (Hp <= 0)
                {
                    GameManager.GetComponent<GameManager>().LifeDown();
                    Destroy(this.gameObject);
                    Instantiate(Explosion, firePos.position, Quaternion.identity);
                }
                else
                {
                    StartCoroutine(getDamage());
                }
            }
        }
       else if (collision.gameObject.tag == "E_Missile")
        {
            if (!isInvinvible)
            {
                Hp -= collision.GetComponent<E_missile>().damage;
                if (Hp <= 0)
                {
                    Instantiate(Explosion, firePos.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    GameManager.GetComponent<GameManager>().LifeDown();
                }
                else
                {
                    StartCoroutine(getDamage());
                }
            }
        }
        /*else if (collision.gameObject.tag == "Enemy")
        {
            if (!isInvinvible)
            {
                Hp -= 10;
                if (Hp <= 0)
                {


                    Instantiate(Explosion, firePos.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    GameManager.GetComponent<GameManager>().LifeDown();
                }
                else
                {
                    StartCoroutine(getDamage());
                }
            }
        }*/

        else if (collision.gameObject.tag == "HpItem")
        {
            if (Hp <= 40)
            {
                Hp += 10;
            }
            else if(Hp<50)
            {
                Hp = 50;
            }
        }
        if (collision.gameObject.tag == "PowerItem")
        {
            if (P_level < 5)
            {
                P_level++;
            }
        }
        else if (collision.gameObject.tag == "ExtraLifeItem")
        {
            GameManager.GetComponent<GameManager>().LifeUp();
        }
        else if (collision.gameObject.tag == "Laser")
        {
            if (!isInvinvible)
            {
                isLaseron = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
            isLaseron = false;
    }
    IEnumerator FireCycleControl()
    {
        bShoot = false;
        yield return new WaitForSeconds(0.1f);
        bShoot = true;
     }
    IEnumerator MissileCycle()
    {
        mShoot = false;
        yield return new WaitForSeconds(1f);
        mShoot = true;
    }
    void Fire()
    {
        if (P_level==1)
        {
            Instantiate(bullet[0], firePos.position, Quaternion.identity);
        }
        if (P_level==2)
        {
            Instantiate(bullet[0], firePos.position, Quaternion.identity);
            Instantiate(bullet[0], firePos.position+ (new Vector3(0.1f, -0.2f, 0)), Quaternion.identity);
            Instantiate(bullet[0], firePos.position-(new Vector3 (0.1f,0.2f,0)), Quaternion.identity);
        }
        if(P_level==3)
        {
            Instantiate(bullet[0], firePos.position, Quaternion.identity);
            Instantiate(bullet[0], firePos.position + (new Vector3(0.1f, -0.2f, 0)), Quaternion.identity);
            Instantiate(bullet[0], firePos.position - (new Vector3(0.1f, 0.2f, 0)), Quaternion.identity);
            Instantiate(bullet[0], firePos.position + (new Vector3(0.2f, -0.4f, 0)), Quaternion.identity);
            Instantiate(bullet[0], firePos.position - (new Vector3(0.2f, 0.4f, 0)), Quaternion.identity);
        }
        if(P_level==4)
        {
            Instantiate(bullet[1], firePos.position, Quaternion.identity);
            Instantiate(bullet[1], firePos.position + (new Vector3(0.1f, -0.2f, 0)), Quaternion.identity);
            Instantiate(bullet[1], firePos.position - (new Vector3(0.1f, 0.2f, 0)), Quaternion.identity);
            Instantiate(bullet[1], firePos.position + (new Vector3(0.2f, -0.4f, 0)), Quaternion.identity);
            Instantiate(bullet[1], firePos.position - (new Vector3(0.2f, 0.4f, 0)), Quaternion.identity);
        }
        if(P_level==5)
        {
            Instantiate(bullet[2], firePos.position, Quaternion.identity);
            Instantiate(bullet[2], firePos.position + (new Vector3(0.1f, -0.2f, 0)), Quaternion.identity);
            Instantiate(bullet[2], firePos.position - (new Vector3(0.1f, 0.2f, 0)), Quaternion.identity);
            Instantiate(bullet[2], firePos.position + (new Vector3(0.2f, -0.4f, 0)), Quaternion.identity);
            Instantiate(bullet[2], firePos.position - (new Vector3(0.2f, 0.4f, 0)), Quaternion.identity);
        }
    }
    public void Expup(int i)
    {   
        Exp += i;
        if (Exp >= 100 && level < 3)
        {
            level++;
            Exp = 0;
        }
    }
    IEnumerator getDamage()
    {
        isInvinvible = true;
        int counttime = 0;
        while(counttime<10)
        {
            if(counttime++%2==0)
            {
                _renderer.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                _renderer.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.15f);
        }
        _renderer.color = new Color32(255, 255, 255, 255);
        yield return null;
        isInvinvible = false;
    }
    IEnumerator P_Spawn()
    {
        isInvinvible = true;
        int counttime = 0;
        while (counttime < 10)
        {
            if (counttime++ % 2 == 0)
            {
                _renderer.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                _renderer.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.3f);
        }
        _renderer.color = new Color32(255, 255, 255, 255);
        yield return null;
        isInvinvible = false;
    }

    public float GetHp()
    {
        return Hp;
    }
    public int GetExp()
    {
        return Exp;
    }
}