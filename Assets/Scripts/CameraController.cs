using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotation = 0;
    public float Clamp = 50;


    void FixedUpdate()
    {
        transform.Rotate(Vector3.right, rotation * Time.fixedDeltaTime);
        Vector3 angles = transform.eulerAngles;
        float diff = (angles.x + 180 + 360) % 360 - 180;
        if (diff < -Clamp)
            angles.x = -Clamp;
        else if (diff > Clamp)
            angles.x = Clamp;
        transform.eulerAngles = angles;
    }

    public void Rotate(float rotation)
    {
        if (rotation > Clamp)
            this.rotation = Clamp;
        else if (rotation < -Clamp)
            this.rotation = -Clamp;
        else
            this.rotation = rotation;
    }
}
