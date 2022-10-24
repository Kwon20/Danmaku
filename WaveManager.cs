using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public GameObject[] Waves;
    private int Num;
    private bool spawn =  false;
	// Use this for initialization
	void Start () {
        Invoke("IsSpawn", 3.0f);
        Num = 0;
	}

    // Update is called once per frame
    void Update() {
        if(spawn && Num<=17)
        {
            StartCoroutine(WaveSpawn());
            Instantiate(Waves[Num]);
        }
        
	}
    private void IsSpawn()
    {
        spawn = true;
    }
    IEnumerator WaveSpawn()
    {
        spawn = false;
        yield return new WaitForSeconds(6.0f);
        Num++;
        spawn = true;
    }
}
