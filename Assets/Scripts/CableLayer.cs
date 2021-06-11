using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CableLayer : MonoBehaviour
{

    public GameObject part;
    public Vector3 Origin = Vector3.zero;
    List<Vector3> points;

    Vector3 LastPoint { get => points.Count > 2 ? points[points.Count - 2] : Origin; }
    Vector3 Target { get => points[points.Count - 1]; set => points[points.Count - 1] = value; }

    List<GameObject> parts;

    void Start()
    {
        points = new List<Vector3>
        {
            Vector3.zero
        };
        parts = new List<GameObject>();
        GameObject part = Instantiate(this.part);
        part.transform.position = LastPoint;
        part.transform.LookAt(Target);
        Vector3 scale = part.transform.localScale;
        scale.z = (Target - Origin).magnitude;
        part.transform.localScale = scale;
        parts.Add(part);
    }


    public void Goto(Vector3 point)
    {
        NavMeshPath path = new NavMeshPath();
        Target = point;
        NavMesh.CalculatePath(LastPoint, Target, NavMesh.AllAreas, path);
        if (path.corners.Length > 2)
        {
            Target = path.corners[1];
            for (int i = 2; i < path.corners.Length; ++i)
                points.Add(path.corners[i]);
        }
        else if (points.Count > 1)
        {
            Vector3 start = points.Count > 2 ? points[points.Count - 2] : Origin;
            NavMesh.CalculatePath(start, Target, NavMesh.AllAreas, path);
            if (path.corners.Length == 2)
            {
                points.RemoveAt(points.Count - 1);
                Goto(point);
            }
        }


        /*GameObject part = parts[0];
        part.transform.position = LastPoint;
        part.transform.LookAt(Target);
        Vector3 scale = part.transform.localScale;
        scale.z = (Target - Origin).magnitude;
        part.transform.localScale = scale;*/
    }

    public void LayCable()
    {

    }

    void Update()
    {
        Debug.DrawLine(Origin, points[0], Color.red);
        for (int i = 0; i < points.Count - 1; i++)
            Debug.DrawLine(points[i], points[i + 1], Color.red);
    }
}
