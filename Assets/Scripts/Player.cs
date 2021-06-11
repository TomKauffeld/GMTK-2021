using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CableLayer))]
public class Player : MonoBehaviour
{

    public float moveSpeed = 5;
    PlayerController controller;
    Camera viewCamera;
    CableLayer myCableLayer;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        myCableLayer = GetComponent<CableLayer>();
        viewCamera = Camera.main;
        myCableLayer.NewCable();
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            controller.LookAt(point);
        }
        myCableLayer.Goto(transform.position + new Vector3(0, -1, 0));
        if (Input.GetKeyDown(KeyCode.Space))
            myCableLayer.LayCable();
    }
}
