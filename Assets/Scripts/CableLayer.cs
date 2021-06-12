using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CableLayer : MonoBehaviour
{

    public GameObject CablePrototype;
    public Transform CableParent = null;
    public float Range = 10;
    public float PointRange = 1;


    public bool HasCable => currentCable != null;

    GameObject currentCable;


    void Start()
    {
        currentCable = null;
    }

    private Tuple<Cable, bool, float> FindClosestAttachedCable(Vector3 point)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Cable.CABLE_PICKUP_TAG);
        Tuple<Cable, bool> closest = null;
        float distance = Mathf.Infinity;
        float rangeMax = Range * Range;
        float pointRangeMax = PointRange * PointRange;
        foreach (GameObject gameObject in gameObjects)
        {
            Cable cable = gameObject.GetComponentInParent<Cable>();
            if (cable == null)
                continue;
            float dstBegin = (transform.position - cable.Begin.transform.position).sqrMagnitude;
            float dstEnd = (transform.position - cable.End.transform.position).sqrMagnitude;
            if (dstBegin < rangeMax)
            {
                float dst = (point - cable.Begin.transform.position).sqrMagnitude;
                if (dst < distance && dst < pointRangeMax)
                {
                    distance = dst;
                    closest = new Tuple<Cable, bool>(cable, true);
                }
            }
            else if (dstEnd < rangeMax)
            {
                float dst = (point - cable.End.transform.position).sqrMagnitude;
                if (dst < distance && dst < pointRangeMax)
                {
                    distance = dst;
                    closest = new Tuple<Cable, bool>(cable, false);
                }
            }
        }
        
        return closest == null ? null : new Tuple<Cable, bool, float>(closest.Item1, closest.Item2, distance);
    }

    private Tuple<GameObject, float> FindClosest(string tag, Vector3 point)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float distance = Mathf.Infinity;
        float rangeMax = Range * Range;
        float pointRangeMax = PointRange * PointRange;
        foreach (GameObject gameObject in gameObjects)
        {
            float dst = (transform.position - gameObject.transform.position).sqrMagnitude;
            if (dst < rangeMax)
            {
                dst = (point - gameObject.transform.position).sqrMagnitude;
                if (dst < distance && dst < pointRangeMax)
                {
                    distance = dst;
                    closestObject = gameObject;
                }
            }
        }
        return closestObject == null ? null : new Tuple<GameObject, float>(closestObject, distance);
    }

    
    public bool PickupCable(Vector3 point)
    {
        Tuple<GameObject, float> closestCable = FindClosest(Cable.CABLE_END_TAG, point);
        Tuple<Cable, bool, float> closestConector = FindClosestAttachedCable(point);
        if (closestConector != null && (closestCable == null || closestCable.Item2 > closestConector.Item3))
        {
            Cable c = closestConector.Item1;
            if (closestConector.Item2)
                c.DetachBegin();
            else
                c.DetachEnd();
            currentCable = c.gameObject;
            c.MakeImaginary();
            return true;
        }
        else if (closestCable != null && (closestConector == null || closestConector.Item3 >= closestCable.Item2))
        {
            Cable c = closestCable.Item1.GetComponentInParent<Cable>();
            if (c == null)
                return false;
            currentCable = c.gameObject;
            c.MakeImaginary();
            return true;
        }
        return false;
    }
    

    public bool NewCable(Vector3 point)
    {
        Tuple<GameObject, float> closest = FindClosest(CableConnector.TAG_CABLE_CONNECTOR, point);
        if (closest == null)
            return false;
        GameObject connectorObject = closest.Item1;
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

    public bool AttachToConnector(Vector3 point)
    {
        Tuple<GameObject, float> closest = FindClosest(CableConnector.TAG_CABLE_CONNECTOR, point);
        if (closest == null)
            return false;
        GameObject connectorObject = closest.Item1;
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
