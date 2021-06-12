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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
