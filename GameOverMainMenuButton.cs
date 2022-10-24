using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMainMenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GoToMainMenu());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Start");
        GameObject Gm = GameObject.FindGameObjectWithTag("GameManager");
        Destroy(Gm);   
    }
}
