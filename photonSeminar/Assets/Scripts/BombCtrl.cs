using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombCtrl : MonoBehaviourPunCallbacks
{
    // Bomb info variables
    private Rigidbody rb;
    public float force = 500f;
    
    // Cache wait for seconds
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(3f);

    void Start()
    {
        // Bomb moves on its own, destroys itself after few seconds
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
        StartCoroutine(DestroyItself());
    }

    // If it hits person, it disappears
    void OnTriggerEnter(Collider coll)
    {
        if(this.tag == "Bomb1" && coll.tag == "Player2")
        {
            Destroy(gameObject);
        }

        if(this.tag == "Bomb2" && coll.tag == "Player1")
        {
            Destroy(gameObject);
        }
    }

    // Bomb destroys itself after few seconds
    IEnumerator DestroyItself()
    {
        yield return waitBeforeDestroy;
        Destroy(gameObject);
    }
}
