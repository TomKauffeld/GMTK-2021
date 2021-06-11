using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void LookAt(Vector3 point)
    {
        Vector3 correctedPoint = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(correctedPoint);
    }

    void FixedUpdate()
    {
        Vector3 pos = myRigidbody.position + velocity * Time.fixedDeltaTime;
        myRigidbody.MovePosition(pos);
    }
}
