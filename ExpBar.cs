using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpBar : MonoBehaviour
{
    private GameObject Player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        this.GetComponent<Slider>().value = Player.GetComponent<PlayerCtrl>().GetExp();
    }
   
}
