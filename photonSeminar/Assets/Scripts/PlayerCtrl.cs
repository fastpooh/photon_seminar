using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCtrl : MonoBehaviourPunCallbacks
{
    // Player spec
    public int myHp = 3;
    private float speedController;
    private Vector3 moveVec;
    private new Transform transform;
    private PhotonView pv;


    // Shooting Bombs
    public GameObject bomb1;
    public GameObject bomb2;

    void Start()
    {
        // Initial settings
        transform = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
        speedController = 3f;
    }

    // float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");

    void Update()
    {
        if(pv.IsMine) // Control only my player
        {
            Move();
            Turn();
            Attack();

            // If two players exist, turn on the scoreboard
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                StartCoroutine(ScoreBoardOn());
            }
        }
    }

    // Same as singleplay script
    void Move()
    {
        if(transform.position.z < -8.2 && vAxis < 0)
            speedController = 0;
        else if(transform.position.z > 8.2 && vAxis > 0)
            speedController = 0;
        else
            speedController = 3f;
        moveVec = new Vector3(0, 0, vAxis);

        transform.position += moveVec*speedController*Time.deltaTime;
    }

    // Same as singleplay script
    void Turn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask layerMask = LayerMask.GetMask("Floor");
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 newForward = hit.point - transform.position;
            newForward.y = 0;
            transform.rotation = Quaternion.LookRotation(newForward);
        }
    }

    // You should sync instantiated bomb
    void Attack()
    {
        if(Input.GetMouseButtonUp(0) && myHp > 0)
        {
            pv.RPC("shoot", RpcTarget.Others); // Tell others that I shot a bomb

            if(this.tag == "Player1")
                Instantiate(bomb1, transform.position, transform.rotation);
            else
                Instantiate(bomb2, transform.position, transform.rotation);
        }
    }

    // If I get hit by a bomb
    void OnTriggerEnter(Collider coll)
    {
        if(pv.IsMine)
        {
            if(this.tag == "Player1" && coll.tag == "Bomb2")
            {
                myHp--;
                if(myHp < 0)
                    myHp = 0;
                pv.RPC("syncHitByBomb", RpcTarget.Others, null);
            }

            if(this.tag == "Player2" && coll.tag == "Bomb1")
            {
                myHp--;
                if(myHp < 0)
                    myHp = 0;
                pv.RPC("syncHitByBomb", RpcTarget.Others, null);
            }
        }
    }

    IEnumerator ScoreBoardOn()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("GameManager").transform.GetChild(0).gameObject.SetActive(true);
    }

    [PunRPC]
    void shoot()
    {
        if(this.tag == "Player1")
        {
            Instantiate(bomb1, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(bomb2, transform.position, transform.rotation);
        }
    }

    [PunRPC]
    void syncHitByBomb()
    {
        myHp--;
    }
}
