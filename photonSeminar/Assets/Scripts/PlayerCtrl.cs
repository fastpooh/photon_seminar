using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCtrl : MonoBehaviourPunCallbacks
{
    public int myHp = 3;
    private Vector3 moveVec;
    private new Transform transform;
    private float speedController;
    private PhotonView pv;

    void Start()
    {
        transform = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
        speedController = 3f;
    }

    // float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");

    void Update()
    {
        if(pv.IsMine)
        {
            Move();
            Turn();
            Attack();

            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                StartCoroutine(ScoreBoardOn());
            }
        }
    }

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

    void Attack()
    {
        if(Input.GetMouseButtonUp(0))
        {
            pv.RPC("shoot", RpcTarget.Others);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(pv.IsMine)
        {
            if(this.tag == "Player1" && coll.tag == "Bomb2")
            {
                myHp--;
                pv.RPC("syncHitByBomb", RpcTarget.Others, null);
            }

            if(this.tag == "Player2" && coll.tag == "Bomb1")
            {
                myHp--;
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
            PhotonNetwork.Instantiate("Bomb1", transform.position, transform.rotation, 0);
        }
        else
        {
            PhotonNetwork.Instantiate("Bomb2", transform.position, transform.rotation, 0);
        }
    }

    [PunRPC]
    void syncHitByBomb()
    {
        myHp--;
    }
}
