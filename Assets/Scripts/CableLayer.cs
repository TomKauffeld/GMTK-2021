using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CableLayer : MonoBehaviour
{

    public GameObject CablePrototype;
    public Transform CableParent = null;
    public float Range = 10;


    public bool HasCable => currentCable != null;

    GameObject currentCable;


    void Start()
    {
        currentCable = null;
    }

    private GameObject FindClosest(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float distance = Mathf.Infinity;
        float rangeMax = Range * Range;
        foreach (GameObject gameObject in gameObjects)
        {
            float dst = (transform.position - gameObject.transform.position).sqrMagnitude;
            if (dst < rangeMax && dst < distance)
            {
                distance = dst;
                closestObject = gameObject;
            }
        }
        return closestObject;
    }

    
    public bool PickupCable()
    {
        GameObject cable = FindClosest(Cable.CABLE_END_TAG);
        if (cable == null)
            return false;
        Cable c = cable.GetComponentInParent<Cable>();
        if (c == null)
            return false;
        currentCable = c.gameObject;
        c.MakeImaginary();
        return true;
    }
    

    public bool NewCable()
    {
        GameObject connectorObject = FindClosest(CableConnector.TAG_CABLE_CONNECTOR);
        if (connectorObject == null)
            return false;
        CableConnector connector = connectorObject.GetComponent<CableConnector>();
        if (connector == null)
            return false;
        if (connector.AttachedCable != null)
            return false;
        currentCable = Instantiate(CablePrototype);
        if (CableParent != null)
            currentCable.transform.SetParent(CableParent);
        Cable cable = currentCable.GetComponent<Cable>();
        cable.Begin = connector;
        connector.AttachedCable = cable;
        return true;
    }

    public bool AttachToConnector()
    {
        GameObject connectorObject = FindClosest(CableConnector.TAG_CABLE_CONNECTOR);
        if (connectorObject == null)
            return false;
        CableConnector connector = connectorObject.GetComponent<CableConnector>();
        if (connector == null)
            return false;
        if (connector.AttachedCable != null)
            return false;
        if (currentCable == null)
            return false;
        Cable cable = currentCable.GetComponent<Cable>();
        cable.Goto(connector.transform.position, true);
        cable.End = connector;
        connector.AttachedCable = cable;
        LayCable();
        return true;
    }

    public void LayCable()
    {
        if (currentCable == null)
            return;
        Cable cable = currentCable.GetComponent<Cable>();
        cable.MakePhysic();
        currentCable = null;
    }

    public void Goto(Vector3 point, bool calculate)
    {
        if (currentCable == null)
            return;
        Cable cable = currentCable.GetComponent<Cable>();
        cable.Goto(point, calculate);
    }

}
