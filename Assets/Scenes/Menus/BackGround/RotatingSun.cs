using UnityEngine;


public class RotatingSun : MonoBehaviour
{
    private static Quaternion? Rotation = null;

    public float TimePerRotation = 60f;

    private void Start()
    {
        if (Rotation.HasValue)
            transform.rotation = Rotation.Value;
    }


    void Update()
    {
        float rotationSpeed = 360 / TimePerRotation * Time.deltaTime;
        transform.Rotate(transform.up, rotationSpeed);
    }

    private void OnDestroy()
    {
        Rotation = transform.rotation;
    }
}
