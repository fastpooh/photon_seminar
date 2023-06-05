using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public PlayerCtrl player1;
    public PlayerCtrl player2;

    //public int player1HP;
    //public int player2HP;

    public TextMeshProUGUI hp1;
    public TextMeshProUGUI hp2;


    void Start()
    {
        player1 = GameObject.FindWithTag("Player1").GetComponent<PlayerCtrl>();  
        player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        hp1.text = "HP : " + player1.myHp;
        hp2.text = "HP : " + player2.myHp;
    }
}
