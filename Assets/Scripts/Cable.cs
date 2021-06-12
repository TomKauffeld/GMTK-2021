using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cable : MonoBehaviour
{
    public const string CABLE_END_TAG = "cable_end";
    public const string CABLE_PICKUP_TAG = "cable_pickup";
    public GameObject Segment;


    public CableConnector Begin = null;
    public CableConnector End = null;


    Vector3 Origin => Begin.transform.position;
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

    public void Goto(Vector3 point, bool calculate, bool direct)
    {
        Target = point;
        if (calculate)
        {
            if (direct)
            {
                points.Clear();
                points.Add(point);
            }
            Vector3 lastPoint = new Vector3(LastPoint.x, 0.1f, LastPoint.z);
            Vector3 target = new Vector3(Target.x, 0.1f, Target.z);
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(lastPoint, target, NavMesh.AllAreas, path);
            if (path.corners.Length > 2)
            {
                Target = path.corners[1];
                for (int i = 2; i < path.corners.Length; ++i)
                    points.Add(path.corners[i]);
            }
            else if (!direct && points.Count > 1)
            {
                Vector3 start = points.Count > 2 ? points[points.Count - 2] : Origin;
                start = new Vector3(start.x, 0.1f, start.z);
                NavMesh.CalculatePath(start, target, NavMesh.AllAreas, path);
                if (path.corners.Length == 2)
                {
                    points.RemoveAt(points.Count - 1);
                    Goto(point, calculate, direct);
                }
            }
        }
        MakeImaginary();
    }


    private void RemoveSegments()
    {
        for (int i = segments.Count - 1; i >= 0; --i)
            Destroy(segments[i]);
        segments.Clear();
    }

    public void MakeImaginary()
    {
        RemoveSegments();
        lr.positionCount = points.Count + 1;
        lr.SetPosition(0, Origin);
        lr.SetPosition(1, points[0]);
        for (int i = 0; i < points.Count - 1; i++)
            lr.SetPosition(i + 2, points[i + 1]);
    }

    private GameObject AddSegmenent(Vector3 start, Vector3 end)
    {
        GameObject segment = Instantiate(Segment);
        segment.transform.position = end;
        segment.transform.LookAt(start);
        Vector3 scale = segment.transform.localScale;

        scale.z = (end - start).magnitude;


        segment.transform.localScale = scale;
        segment.transform.SetParent(transform);
        segments.Add(segment);
        return segment;
    }

    public void MakePhysic()
    {
        RemoveSegments();
        AddSegmenent(Origin, points[0]);
        for (int i = 0; i < points.Count - 1; i++)
            AddSegmenent(points[i], points[i + 1]);

        if (End == null)
            segments[segments.Count - 1].tag = CABLE_END_TAG;
        else
        {
            segments[0].tag = CABLE_PICKUP_TAG;
            segments[segments.Count - 1].tag = CABLE_PICKUP_TAG;
        }

        lr.positionCount = 0;
    }

    private void InverseCable()
    {
        RemoveSegments();
        CableConnector tmp = Begin;
        Begin = End;
        End = tmp;
        List<Vector3> points = new List<Vector3>();
        for (int i = this.points.Count - 1; i >= 0; --i)
            points.Add(this.points[i]);
        this.points = points;
    }


    public void DetachBegin()
    {
        InverseCable();
        DetachEnd();
    }

    public void DetachEnd()
    {
        RemoveSegments();
        if (End == null)
            return;
        End.AttachedCable = null;
        End = null;
    }
}
