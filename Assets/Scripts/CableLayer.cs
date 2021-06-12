using System;
using UnityEngine;

public class CableLayer : MonoBehaviour
{

    public float MaxRange = 10;

    public GameObject CablePrototype;
    public Transform CableParent = null;
    public float Range = 10;
    public float PointRange = 1;
    public bool Direct = false;

    public float CurrentLength => currentCable != null ? currentCable.GetComponent<Cable>().Length : 0;


    public bool HasCable => currentCable != null;

    GameObject currentCable;


    void Start()
    {
        currentCable = null;
    }

    public void DeleteCable()
    {
        if (currentCable != null)
        {
            Destroy(currentCable);
            currentCable = null;
        }
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
        Tuple<GameObject, float> closestConnector = FindClosest(CableConnector.TAG_CABLE_CONNECTOR, point);
        if (closestConnector != null && (closestCable == null || closestCable.Item2 > closestConnector.Item2))
        {
            CableConnector connector = closestConnector.Item1.GetComponent<CableConnector>();
            if (connector.AttachedCable != null)
            {
                Cable cable = connector.AttachedCable;
                if (cable.End != null)
                {
                    if (cable.Begin == connector)
                        cable.DetachBegin();
                    else
                        cable.DetachEnd();
                    currentCable = cable.gameObject;
                    cable.MakeImaginary();
                }
            }
            else
            {
                currentCable = Instantiate(CablePrototype);
                if (CableParent != null)
                    currentCable.transform.SetParent(CableParent);
                Cable cable = currentCable.GetComponent<Cable>();
                cable.MaxLength = MaxRange;
                cable.Begin = connector;
                connector.AttachedCable = cable;
            }
            return true;
        }
        else if (closestCable != null && (closestConnector == null || closestConnector.Item2 >= closestCable.Item2))
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
        cable.Goto(connector.transform.position, true, Direct);
        if (cable.Length <= cable.MaxLength)
        {
            cable.End = connector;
            connector.AttachedCable = cable;
            LayCable();
            return true;
        }
        else
            return false;
    }

    public bool LayCable()
    {
        if (currentCable == null)
            return false;
        Cable cable = currentCable.GetComponent<Cable>();
        if (cable.Length <= cable.MaxLength)
        {
            cable.MakePhysic();
            currentCable = null;
            return true;
        }
        return false;
    }

    public void Goto(Vector3 point, bool calculate)
    {
        if (currentCable == null)
            return;
        Cable cable = currentCable.GetComponent<Cable>();
        cable.Goto(point, calculate, Direct);
    }

}
