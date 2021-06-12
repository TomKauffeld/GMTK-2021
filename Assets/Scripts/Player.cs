using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CableLayer))]
public class Player : MonoBehaviour
{


    public float interval = 0.1f;
    public float moveSpeed = 5;
    public float rotationSpeed = 5;
    PlayerController controller;
    Camera viewCamera;
    CableLayer myCableLayer;
    MainHud hud;

    float elapsed = 0;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        myCableLayer = GetComponent<CableLayer>();
        hud = GetComponentInChildren<MainHud>();
        viewCamera = Camera.main;
    }

    private void CheckCableInteraction(Vector3 point)
    {

        if (InputManager.GetPrimaryDown())
        {
            if (myCableLayer.HasCable)
            {
                if (!myCableLayer.AttachToConnector(point))
                    myCableLayer.LayCable();
            }
            else
            {
                myCableLayer.PickupCable(point);
            }
        }
        else if (InputManager.GetSecondaryDown())
        {
            myCableLayer.DeleteCable();
        }
    }

    void Update()
    {
        if (InputManager.GetActionDown(Actions.PAUSE))
            hud.Pause = !hud.Pause;

        if (hud.Pause)
        {

        }
        else
        {
            elapsed += Time.deltaTime;

            Vector3 moveInput = InputManager.GetMovement();
            Vector3 moveVelocity = moveInput.normalized * moveSpeed;
            controller.Move(moveVelocity);

            Vector3 rotation = Vector3.zero;
            if (InputManager.GetAction(Actions.TURN_LEFT))
                rotation.y -= rotationSpeed;
            else if (InputManager.GetAction(Actions.TURN_RIGHT))
                rotation.y += rotationSpeed;
            controller.Rotate(rotation);





            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 point = new Vector3(hit.point.x, 0.1f, hit.point.z);
                Vector3 target = point;
                Vector3 diff = point - (transform.position + new Vector3(0, -1, 0));
                if (diff.sqrMagnitude >= myCableLayer.Range * myCableLayer.Range)
                    target = transform.position + new Vector3(0, -1, 0) + diff.normalized * myCableLayer.Range;

                if (elapsed > interval)
                {
                    myCableLayer.Goto(target, true);
                    elapsed -= interval;
                }
                else
                    myCableLayer.Goto(target, false);

                CheckCableInteraction(point);
            }
        }
    }
}
