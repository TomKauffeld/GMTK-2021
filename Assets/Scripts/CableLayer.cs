using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CableLayer : MonoBehaviour
{

    public GameObject CablePrototype;
    public float Range;

    GameObject currentCable;


    void Start()
    {
        currentCable = null;
    }

    
    public bool PickupCable()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, Range, Vector3.forward, out hit);

        return true;
    }
    

    public void NewCable()
    {
        currentCable = Instantiate(CablePrototype);
    }

    public void Goto(Vector3 point)
    {
        if (currentCable == null)
            return;
        Cable cable = currentCable.GetComponent<Cable>();
        cable.Goto(point);
    }

    public void LayCable()
    {
        if (currentCable == null)
            return;
        Cable cable = currentCable.GetComponent<Cable>();
        cable.MakePhysic();
    }

}
