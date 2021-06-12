using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInputManager : MonoBehaviour
{
    public enum LayoutEnum
    {
        QWERTY,
        AZERTY
    }
    protected abstract KeyCode GetKeyCodeForAction(Actions action);

    public LayoutEnum Layout = LayoutEnum.QWERTY;

    private bool lastPrimary = false;
    private bool lastSecondary = false;

    public Vector3 GetMovement()
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

    public bool GetPrimaryDown()
    {
        bool primary = Input.GetAxisRaw("Primary") > 0 || GetActionDown(Actions.PRIMARY);
        bool ret = primary && !lastPrimary;
        lastPrimary = primary;
        return ret;
    }

    public bool GetSecondaryDown()
    {
        bool secondary = Input.GetAxisRaw("Secondary") > 0 || GetActionDown(Actions.SECONDARY);
        bool ret = secondary && !lastSecondary;
        lastSecondary = secondary;
        return ret;
    }

    public bool GetAction(Actions actions) => Input.GetKey(GetKeyCodeForAction(actions));

    public bool GetActionDown(Actions action) => Input.GetKeyDown(GetKeyCodeForAction(action));


    public void SwitchLayout()
    {
        Layout = Layout == LayoutEnum.AZERTY ? LayoutEnum.QWERTY : LayoutEnum.AZERTY;
    }
}
