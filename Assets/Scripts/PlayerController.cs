using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    public void LookAt(Vector3 point)
    {
        Vector3 correctedPoint = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(correctedPoint);
    }

    void FixedUpdate()
    {
        Vector3 rotation = myRigidbody.rotation.eulerAngles + this.rotation * Time.fixedDeltaTime;
        myRigidbody.MoveRotation(Quaternion.Euler(rotation));
        if (velocity.sqrMagnitude > 0.1f)
        {
            Vector3 pos = myRigidbody.position + transform.rotation * velocity * Time.fixedDeltaTime;
            myRigidbody.MovePosition(pos);
        }
        else
            myRigidbody.velocity = Vector3.zero;
    }
}
