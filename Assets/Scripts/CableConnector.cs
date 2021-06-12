using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableConnector : MonoBehaviour
{
    private static int NEXT_ID = 1;

    public readonly int Id = NEXT_ID++;

    public const string TAG_CABLE_CONNECTOR = "cable_connector";

    public Cable AttachedCable { get => attachedCable; set => SetAttachedCable(value); }


    Cable attachedCable = null;

    public void SetAttachedCable(Cable cable)
    {
        if (cable == null)
            DetachCable();
        else
            AttachCable(cable);
    }

    private void AttachCable(Cable cable)
    {
        attachedCable = cable;
    }

    private void DetachCable()
    {
        Cable cable = attachedCable;
        if (cable != null)
        {
            attachedCable = null;
            if (cable.Begin == this)
                cable.DetachBegin();
            else if (cable.End == this)
                cable.DetachEnd();
        }
    }

    private void AddConnectorToList(List<CableConnector> connections, CableConnector connector)
    {
        if (connections.Contains(connector))
            return;
        connections.Add(connector);
        connector.PrivateGetConnections(connections);
    }

    private List<CableConnector> PrivateGetConnections(List<CableConnector> connections = null)
    {
        if (connections == null)
            connections = new List<CableConnector>();
        if (!connections.Contains(this))
            connections.Add(this);

        if (attachedCable != null)
        {
            if (attachedCable.Begin == this && attachedCable.End != null)
                AddConnectorToList(connections, attachedCable.End);
            else if (attachedCable.End == this)
                AddConnectorToList(connections, attachedCable.Begin);
        }

        Switch s = GetComponentInParent<Switch>();

        if (s != null)
        {
            CableConnector[] others = s.GetComponentsInChildren<CableConnector>();
            foreach(CableConnector connector in others)
                AddConnectorToList(connections, connector);
        }

        return connections;
    }

    public List<CableConnector> GetConnections()
    {
        return PrivateGetConnections();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
    }
}