using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {
    public bool D_HP = false;
    public bool D_Power = false;
    public bool D_ExtraLife = false;
    public Transform[] DropPos;
    public GameObject hp;
    public GameObject power;
    public GameObject EL;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ItemDrop()
    {
        if(D_HP)
        {
            Instantiate(hp, DropPos[0].position, Quaternion.identity);
        }

        if (D_Power)
        {
            Instantiate(power, DropPos[1].position, Quaternion.identity);
        }

        if (D_ExtraLife)
        {
            Instantiate(EL, DropPos[2].position, Quaternion.identity);
        }
    }
}
