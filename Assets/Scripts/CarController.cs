using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{

    public float Speed = 5;

    Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 pos = myRigidbody.position + transform.rotation * (Speed * Time.fixedDeltaTime * Vector3.forward);
        myRigidbody.MovePosition(pos);
    }

}
