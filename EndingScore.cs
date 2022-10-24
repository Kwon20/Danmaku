using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndingScore : MonoBehaviour {
    private GameObject GameManager;
    private Text text;
    private Text Time_Text;
    public GameObject Score;
    public GameObject Time;
    // Use this for initialization
    void Start()
    {
        Time_Text = Time.GetComponent<Text>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        text = Score.GetComponent<Text>();
        text.text = "Score : " + GameManager.GetComponent<GameManager>().GetScore();
        Time_Text.text= "Time : " + GameManager.GetComponent<GameManager>().GetTime().ToString("N2");
        //text.text= "Time : " + GameManager.GetComponent<GameManager>().GetTime().ToString("N2");
        Destroy(GameManager.gameObject);
    }
   
   
}
