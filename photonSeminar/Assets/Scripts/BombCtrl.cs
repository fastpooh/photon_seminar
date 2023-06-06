using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombCtrl : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    public float force = 500f;
    private PhotonView pv;
    private ScoreBoard sc;
    
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(3f);

    void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
        sc = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
        StartCoroutine(DestroyItself());
    }

    void OnTriggerEnter(Collider coll)
    {
        if(this.tag == "Bomb1" && coll.tag == "Player2")
        {
            Destroy(gameObject);
            sc.HP2--;
        }

        if(this.tag == "Bomb2" && coll.tag == "Player1")
        {
            Destroy(gameObject);
            sc.HP1--;
        }
    }

    IEnumerator DestroyItself()
    {
        yield return waitBeforeDestroy;
        Destroy(gameObject);
    }
}
