using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Vector3 moveVec;
    private new Transform transform;
    private float speedController;

    void Start()
    {
        transform = GetComponent<Transform>();
        speedController = 3f;
    }

    // float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");

    void Update()
    {
        Move();
        Turn();
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
}
