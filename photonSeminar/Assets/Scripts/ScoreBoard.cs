using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoard : MonoBehaviourPunCallbacks
{
    public PlayerCtrl player1;
    public PlayerCtrl player2;

    public GameObject exitUI;
    public TextMeshProUGUI winnerLog;

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

        if(player1.myHp == 0)
        {
            exitUI.SetActive(true);
            winnerLog.text = "Blue Player Wins";
        }
        else if(player2.myHp == 0)
        {
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
