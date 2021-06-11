using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CableLayer : MonoBehaviour
{

    public GameObject CablePrototype;
    public Transform CableParent = null;
    public float Range = 10;

    GameObject currentCable;


    void Start()
    {
        currentCable = null;
    }

    
    public bool PickupCable()
    {

        GameObject[] objects = GameObject.FindGameObjectsWithTag(Cable.CABLE_END_TAG);
        GameObject cable = null;
        float distance = Mathf.Infinity;
        float rangeMax = Range * Range;
        foreach(GameObject gameObject in objects)
        {

            Vector3 position = gameObject.transform.position;
            float dst = (transform.position - position).sqrMagnitude;
            if (dst < rangeMax && dst < distance)
            {
                distance = dst;
                cable = gameObject;
            }
        }
        if (cable == null)
            return false;
        Cable c = cable.GetComponentInParent<Cable>();
        if (c == null)
            return false;
        currentCable = c.gameObject;
        c.MakeImaginary();
        return true;
    }
    

    public void NewCable()
    {
        currentCable = Instantiate(CablePrototype);
        if (CableParent != null)
            currentCable.transform.SetParent(CableParent);
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
        currentCable = null;
    }

}
