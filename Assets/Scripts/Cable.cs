using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cable : MonoBehaviour
{
    public GameObject Segment;
    public Vector3 Origin = Vector3.zero;
    LineRenderer lr;
    List<Vector3> points;

    Vector3 LastPoint { get => points.Count > 2 ? points[points.Count - 2] : Origin; }
    Vector3 Target { get => points[points.Count - 1]; set => points[points.Count - 1] = value; }



    List<GameObject> segments;

    void Start()
    {
        points = new List<Vector3>
        {
            Vector3.zero
        };
        segments = new List<GameObject>();
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.sharedMaterial.SetColor("_Color", Color.yellow);
        lr.startWidth = 0.25f;
        lr.endWidth = 0.25f;
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



        lr.positionCount = points.Count + 1;
        lr.SetPosition(0, Origin);
        lr.SetPosition(1, points[0]);
        for (int i = 0; i < points.Count - 1; i++)
            lr.SetPosition(i + 2, points[i + 1]);

    }

    private GameObject AddSegmenent(Vector3 start, Vector3 end)
    {
        GameObject segment = Instantiate(Segment);
        segment.transform.position = start;
        segment.transform.LookAt(end);
        Vector3 scale = segment.transform.localScale;

        scale.z = (end - start).magnitude;


        segment.transform.localScale = scale;
        return segment;
    }

    public void MakePhysic()
    {
        for(int i = segments.Count - 1; i >= 0; --i)
            Destroy(segments[i]);
        segments.Clear();

        segments.Add(AddSegmenent(Origin, points[0]));
        for (int i = 0; i < points.Count - 1; i++)
            segments.Add(AddSegmenent(points[i], points[i + 1]));
        segments[segments.Count - 1].tag = "end_cable";
    }
}
