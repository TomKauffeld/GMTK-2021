using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CableLayer))]
public class Player : MonoBehaviour
{


    public float interval = 0.1f;
    public float moveSpeed = 5;
    public float rotationSpeed = 1;
    PlayerController controller;
    Camera viewCamera;
    CameraController cameraController;
    CableLayer myCableLayer;
    MainHud hud;

    float elapsed = 0;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        myCableLayer = GetComponent<CableLayer>();
        hud = GetComponentInChildren<MainHud>();
        viewCamera = Camera.main;
        cameraController = viewCamera.GetComponent<CameraController>();
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

    private IHighlightable highlightable = null;

    void Update()
    {
        if (InputManager.GetActionDown(Actions.PAUSE))
            hud.Pause = !hud.Pause;

        if (hud.Pause)
        {
            if (Cursor.lockState != CursorLockMode.None)
                Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            if (Cursor.lockState != CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            elapsed += Time.deltaTime;

            Vector3 moveInput = InputManager.GetMovement();
            Vector3 moveVelocity = moveInput.normalized * moveSpeed;
            controller.Move(moveVelocity);

            Vector3 rotation = InputManager.GetRotation();
            Vector3 rotationVelocity = rotation * rotationSpeed;
            controller.Rotate(Vector3.up * rotationVelocity.y);
            cameraController.Rotate(rotationVelocity.x);





            Ray ray = new Ray(viewCamera.transform.position, viewCamera.transform.rotation * Vector3.forward);
            //Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (highlightable != null)
                    highlightable.SetHighlight(false);
                highlightable = hit.collider.GetComponent<IHighlightable>();
                if (highlightable == null)
                    highlightable = hit.collider.GetComponentInParent<IHighlightable>();
                if (highlightable != null)
                    highlightable.SetHighlight(true);

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

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
