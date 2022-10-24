using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour
{
    public Transform[] firePos;
    public Transform[] SpawnPos;
    private bool isSecondPhase=false;
    public int B_Hp;
    public int pattern=0;
    public bool isPlayPattern;
    private bool left = true;
    public GameObject GameManager;
    private int MaxHP = 30000;
    private float P1_time;
    private int shot_try = 0;
    public GameObject[] bullet;
    private GameObject Target;
    public GameObject Egg;
    public GameObject[] Laser;
    // Use this for initialization
    void Start()
    {
        //   pattern = 1;
        // StartCoroutine(PatternCycle());
        B_Hp = MaxHP;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        isSecondPhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((float)((float)B_Hp/(float)MaxHP)<=0.5f)
        { isSecondPhase = true; }
        if(pattern!=5)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 3, 0), 0.02f);
        }
        if(isPlayPattern)
        {
            if (pattern == 1)
            {
                P1_time += Time.deltaTime;
                if ((P1_time > 1f && P1_time < 1.02f) || (P1_time > 1.1f && P1_time < 1.12f) || (P1_time > 1.2f && P1_time < 1.22f))
                {
                    for (int i = 0; i < 20; i++)
                    {
                        GameObject obj;
                        obj = (GameObject)Instantiate(bullet[0], firePos[0].position, Quaternion.identity);
                        obj.transform.Rotate(new Vector3(0, 0, 360 * i / 20 -45*shot_try++));
                        
                    }
                }
             }
            if(pattern==2)
            {
                P1_time += Time.deltaTime;
                if ((P1_time > 1f && P1_time < 1.02f) || (P1_time > 1.2f && P1_time < 1.22f) || (P1_time > 1.4f && P1_time < 1.42f) || (P1_time > 1.6f && P1_time < 1.62f))
                {
                    Instantiate(bullet[1], firePos[1].position, Quaternion.Euler(0, 0, 180));
                    Instantiate(bullet[1], firePos[2].position, Quaternion.Euler(0,0,180));
                }
            }
            if(pattern==3)
            {
                P1_time += Time.deltaTime;
                if(P1_time>1f&&P1_time<1.02f)
                {
                    Instantiate(Egg, SpawnPos[0].position, Quaternion.identity);
                    Instantiate(Egg, SpawnPos[1].position, Quaternion.identity);
                    Instantiate(Egg, SpawnPos[2].position, Quaternion.identity);
                    Instantiate(Egg, SpawnPos[3].position, Quaternion.identity);
                }
            }
            if(pattern==4)
            {
                P1_time += Time.deltaTime;
                if(P1_time>1f&&P1_time<3.0f)
                {
                    for (int i = 0; i < 3; i++)
                    {

                        GameObject obj;
                        obj = (GameObject)Instantiate(bullet[2], firePos[3].position, firePos[3].rotation);
                        obj.transform.Rotate(new Vector3(firePos[3].rotation.x, firePos[3].rotation.y, firePos[3].rotation.z + 120 * i));
                    }
                }
            }
            if(pattern==5)
            {
                P1_time += Time.deltaTime;
                if (P1_time > 1f && P1_time < 2.5f)
                {
                    if (left)
                        transform.Translate(Vector2.left * Time.deltaTime);
                    else
                        transform.Translate(Vector2.right * Time.deltaTime);
                    Laser[0].SetActive(true);
                    Laser[1].SetActive(true);
                }
                else
                {
                    Laser[0].SetActive(false);
                    Laser[1].SetActive(false);
                }
            }
        }
        else
        {
            if(isSecondPhase==true)
            {
                float x = Random.Range(0, 5);
                if (x < 1)
                    pattern = 1;
                else if (x < 2)
                    pattern = 2;
                else if (x < 3)
                    pattern = 3;
                else if (x < 4)
                    pattern = 4;
                else if (x < 5)
                    pattern = 5;
                StartCoroutine(PatternCycle());
            }
            else
            {
                float x = Random.Range(0, 3);
                if (x < 1)
                    pattern = 1;
                else if (x < 2)
                    pattern = 2;
                else if (x < 3)
                    pattern = 3;
                StartCoroutine(PatternCycle());

            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            B_Hp -= collision.GetComponent<BulletMove>().damage;
            Destroy(collision.gameObject);
            if (B_Hp <= 0)
            {
                Destroy(gameObject);
                GameManager.GetComponent<GameManager>().Finish();
                SceneManager.LoadScene("Ending");
            }
        }
        if (collision.tag == "Missile")
        {
            B_Hp -= collision.GetComponent<MissileMove>().damage;
            Destroy(collision.gameObject);
            if (B_Hp <= 0)
            {
                Destroy(gameObject);
                GameManager.GetComponent<GameManager>().Finish();
            }
        }
    }
    
    IEnumerator PatternCycle()
    {
      
        float x = Random.Range(0, 2);
        if (x < 1)
            left = true;
        else
            left = false;
        isPlayPattern = true;
        yield return new WaitForSeconds(3f);
        isPlayPattern = false;
        P1_time = 0;
    }
   
}
