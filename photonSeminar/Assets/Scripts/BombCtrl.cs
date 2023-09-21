using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombCtrl : MonoBehaviourPunCallbacks
{
    // Bomb related Data
    private Rigidbody rb;
    public float force = 500f;
    // Scoreboard
    private ScoreBoard sc;
    // Cache WaitForSeconds
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(3f);

    void Start()
    {
        // Shoot bomb
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);

        // Find scoreboard
        sc = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();

        // Destory itself after 3 seconds
        StartCoroutine(DestroyItself());
    }

    // When bomb hits player
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
