using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Vector3 moveVec;

    void Start()
    {
        
    }

    // float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");

    void Update()
    {
        
    }

    void Move()
    {
        moveVec = new Vector3(0, 0, vAxis);
        transform.position += moveVec*Time.deltaTime;
    }
}
