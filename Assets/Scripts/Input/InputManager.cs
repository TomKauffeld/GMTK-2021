using System.Collections.Generic;
using UnityEngine;

public class InputManager : AInputManager
{

    private static readonly Dictionary<LayoutEnum, Dictionary<Actions, KeyCode>> Keys = new Dictionary<LayoutEnum, Dictionary<Actions, KeyCode>>
    {
        {
            LayoutEnum.QWERTY, new Dictionary<Actions, KeyCode>
            {
                { Actions.MOVE_FORWARD, KeyCode.W },
                { Actions.MOVE_BACKWARD, KeyCode.S },
                { Actions.MOVE_LEFT, KeyCode.A },
                { Actions.MOVE_RIGHT, KeyCode.D },
                { Actions.TURN_LEFT, KeyCode.Q },
                { Actions.TURN_RIGHT, KeyCode.E },
                { Actions.PRIMARY, KeyCode.Space },
                { Actions.SECONDARY, KeyCode.Delete },
                { Actions.CHANGE_LAYOUT, KeyCode.Tab },
            }
        },
        {
            LayoutEnum.AZERTY, new Dictionary<Actions, KeyCode>
            {
                { Actions.MOVE_FORWARD, KeyCode.Z },
                { Actions.MOVE_BACKWARD, KeyCode.S },
                { Actions.MOVE_LEFT, KeyCode.Q },
                { Actions.MOVE_RIGHT, KeyCode.D },
                { Actions.TURN_LEFT, KeyCode.A },
                { Actions.TURN_RIGHT, KeyCode.E },
                { Actions.PRIMARY, KeyCode.Space },
                { Actions.SECONDARY, KeyCode.Delete },
                { Actions.CHANGE_LAYOUT, KeyCode.Tab },
            }
        }
    };



    protected override KeyCode GetKeyCodeForAction(Actions action) => Keys[Layout][action];


}
