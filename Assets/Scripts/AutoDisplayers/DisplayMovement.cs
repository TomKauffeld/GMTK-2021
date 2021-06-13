using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayMovement : MonoBehaviour
{
    private string movementFormat;
    private Text box;

    void Start()
    {
        box = GetComponent<Text>();
        movementFormat = box.text;
    }

    void Update()
    {
        box.text = string.Format(movementFormat,
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_FORWARD),
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_BACKWARD),
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_LEFT),
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_RIGHT),
            KeyCodesDictionary.GetNameForAction(Actions.TURN_LEFT),
            KeyCodesDictionary.GetNameForAction(Actions.TURN_RIGHT)
        );
    }
}
