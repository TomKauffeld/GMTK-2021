using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    Vector3 initial;

    void Start()
    {
        initial = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (target)
        {
            Vector3 translation = target.position - transform.position + initial;
            
            transform.position += translation;
        }
    }
}
