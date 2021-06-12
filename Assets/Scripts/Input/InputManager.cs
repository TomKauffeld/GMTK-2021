using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static readonly Dictionary<Layout, Dictionary<Actions, KeyCode>> Keys = new Dictionary<Layout, Dictionary<Actions, KeyCode>>
    {
        {
            Layout.QWERTY, new Dictionary<Actions, KeyCode>
            {
                { Actions.MOVE_FORWARD, KeyCode.W },
                { Actions.MOVE_BACKWARD, KeyCode.S },
                { Actions.MOVE_LEFT, KeyCode.A },
                { Actions.MOVE_RIGHT, KeyCode.D },
                { Actions.TURN_LEFT, KeyCode.Q },
                { Actions.TURN_RIGHT, KeyCode.E },
                { Actions.PRIMARY, KeyCode.Space },
                { Actions.SECONDARY, KeyCode.Delete },
                { Actions.PAUSE, KeyCode.P }
            }
        },
        {
            Layout.AZERTY, new Dictionary<Actions, KeyCode>
            {
                { Actions.MOVE_FORWARD, KeyCode.Z },
                { Actions.MOVE_BACKWARD, KeyCode.S },
                { Actions.MOVE_LEFT, KeyCode.Q },
                { Actions.MOVE_RIGHT, KeyCode.D },
                { Actions.TURN_LEFT, KeyCode.A },
                { Actions.TURN_RIGHT, KeyCode.E },
                { Actions.PRIMARY, KeyCode.Space },
                { Actions.SECONDARY, KeyCode.Delete },
                { Actions.PAUSE, KeyCode.P }
            }
        }
    };




    private static bool lastPrimary = false;
    private static bool lastSecondary = false;

    public static Vector3 GetMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (GetAction(Actions.MOVE_FORWARD))
            moveInput.z += 1;
        if (GetAction(Actions.MOVE_BACKWARD))
            moveInput.z -= 1;
        if (GetAction(Actions.MOVE_LEFT))
            moveInput.x -= 1;
        if (GetAction(Actions.MOVE_RIGHT))
            moveInput.x += 1;
        return moveInput;
    }

    public static Vector3 GetRotation()
    {
        float pitch = Input.GetAxisRaw("Pitch") * (Settings.InversePitch ? 1 : -1) * Settings.Sensibility * 15;
        float yaw = Input.GetAxisRaw("Yaw") * (Settings.InverseYaw ? -1 : 1) * Settings.Sensibility * 15;
        Vector3 rotation = new Vector3(pitch, yaw, 0);
        if (GetAction(Actions.TURN_LEFT))
            rotation.y -= 90;
        if (GetAction(Actions.TURN_RIGHT))
            rotation.y += 90;
        return rotation;
    }


    public static bool GetPrimaryDown()
    {
        bool primary = Input.GetAxisRaw("Primary") > 0 || GetActionDown(Actions.PRIMARY);
        bool ret = primary && !lastPrimary;
        lastPrimary = primary;
        return ret;
    }

    public static bool GetSecondaryDown()
    {
        bool secondary = Input.GetAxisRaw("Secondary") > 0 || GetActionDown(Actions.SECONDARY);
        bool ret = secondary && !lastSecondary;
        lastSecondary = secondary;
        return ret;
    }

    protected static KeyCode GetKeyCodeForAction(Actions action) => Keys[Settings.Layout][action];

    public static bool GetAction(Actions actions) => Input.GetKey(GetKeyCodeForAction(actions));

    public static bool GetActionDown(Actions action) => Input.GetKeyDown(GetKeyCodeForAction(action));

}
