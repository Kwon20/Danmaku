using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        text.color = new Color32(255, 0, 0, 0);
        StartCoroutine(TextFadeIN());
	}
	
	// Update is called once per frame
	void Update () {
	   
	}
    IEnumerator TextFadeIN()
    {
       
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime/2f));
            yield return null;
        
        }
    }
  
}
