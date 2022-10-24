using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private short Life = 3;
    private int score;
    public GameObject Player;
    public Transform RespawnTrans;
    public GameObject gameover;
    public float Timer;
    private bool TimeUP;
    public GameObject[] Button;
    // Use this for initialization
    void Awake()
    {
        GameObject[] Gm;
        
            Gm = GameObject.FindGameObjectsWithTag("GameManager");
        for(int l=0;l<Gm.Length-1;l++)
        {
            Destroy(Gm[l].gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        TimeUP = true;
    }
    void Start () {
        score = 0;
        Timer = 0;
        Screen.SetResolution(1200, 1800, true);
	}
	
	// Update is called once per frame
	void Update () {
        if(TimeUP)
        {
            Timer += Time.deltaTime;
        }
		if(Input.GetKeyDown(KeyCode.K))
        { LifeDown(); }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Finish();
        }
	}
    public void LifeUp()
    {
        Life++;
    }
    public void LifeDown()
    {
       if(Life>=1)
        {
            Life--;
            StartCoroutine(Respawn());
        }
       else if(Life<=0)
        { Failed(); }
    }
    public void ScoreUp(int E_score)
    {
        score += E_score;
    }
    public void Finish()
    {
        TimeUP = false;
        SceneManager.LoadScene("Ending");
        //클리어창
    }
    public void Failed()
    {
        
        //Time.timeScale = 0;
        gameover.SetActive(true);
        Button[0].SetActive(true);
        Button[1].SetActive(true);
        TimeUP = false;
    }
    public int GetScore()
    { return score; }
    public void scoreUP(int i)
    {
        score += i;
    }
    public float GetTime()
    {
        return Timer;
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Player, RespawnTrans.position, Quaternion.identity);
    }
}
