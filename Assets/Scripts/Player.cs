using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CableLayer))]
public class Player : MonoBehaviour
{

    public float interval = 0.1f;
    public float moveSpeed = 5;
    PlayerController controller;
    Camera viewCamera;
    CableLayer myCableLayer;


    bool lastIsPressed = false;
    float elapsed = 0;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        myCableLayer = GetComponent<CableLayer>();
        viewCamera = Camera.main;
    }

    private void CheckCableInteraction(Vector3 point)
    {
        bool isPressed = Input.GetKeyDown(Settings.CableInteraction);

        if (isPressed && !lastIsPressed)
        {
            if (myCableLayer.HasCable)
            {
                if (!myCableLayer.AttachToConnector(point))
                    myCableLayer.LayCable();
            }
            else
            {
                if (!myCableLayer.PickupCable(point))
                    myCableLayer.NewCable(point);
            }
        }
        lastIsPressed = Input.GetKeyDown(Settings.CableInteraction);
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, transform.position.y - 1, 0));
        plane.Raycast(ray, out float rayDistance);
        Vector3 point = ray.GetPoint(rayDistance);
        controller.LookAt(point);


        Vector3 target = point;
        Vector3 diff = point - (transform.position + new Vector3(0, -1, 0));
        if (diff.sqrMagnitude >= myCableLayer.Range * myCableLayer.Range)
        {
            target = transform.position + new Vector3(0, -1, 0) + diff.normalized * myCableLayer.Range;
        }

        if (elapsed > interval)
        {
            myCableLayer.Goto(target, true);
            elapsed -= interval;
        }
        else
            myCableLayer.Goto(target, false);

        CheckCableInteraction(point);
    }
}
