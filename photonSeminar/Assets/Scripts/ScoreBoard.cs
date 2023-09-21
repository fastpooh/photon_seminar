using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoard : MonoBehaviourPunCallbacks
{
    public GameObject exitUI;
    public TextMeshProUGUI winnerLog;
    public TextMeshProUGUI hp1;
    public TextMeshProUGUI hp2;

    public int HP1 = 3;
    public int HP2 = 3;

    //public PlayerCtrl player1;
    //public PlayerCtrl player2;


    void Start()
    {
        //player1 = GameObject.FindWithTag("Player1").GetComponent<PlayerCtrl>();
        //player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update scoreboard hp
        hp1.text = "HP : " + HP1;
        hp2.text = "HP : " + HP2;

        // Detect winner
        if(HP1 <= 0)
        {
            HP1 = 0;
            exitUI.SetActive(true);
            winnerLog.text = "Blue Player Wins";
        }
        else if(HP2 <= 0)
        {
            HP2 = 0;
            exitUI.SetActive(true);
            winnerLog.text = "Red Player Wins";
        }
        
    }


    public void OnExitClick()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("StartUI");
    }
}
