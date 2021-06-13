using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHelpController : MonoBehaviour
{

    public Text MovementText;
    public Text InteractionText;

    private AsyncOperation Operation = null;


    private string movementFormat;
    private string interactionFormat;

    private void Start()
    {
        movementFormat = MovementText.text;
        interactionFormat = InteractionText.text;
    }


    void Update()
    {
        if (Operation != null && Operation.isDone)
            Operation = null;

        MovementText.text = string.Format(movementFormat,
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_FORWARD),
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_BACKWARD),
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_LEFT),
            KeyCodesDictionary.GetNameForAction(Actions.MOVE_RIGHT),
            KeyCodesDictionary.GetNameForAction(Actions.TURN_LEFT),
            KeyCodesDictionary.GetNameForAction(Actions.TURN_RIGHT)
        );

        InteractionText.text = string.Format(interactionFormat,
            KeyCodesDictionary.GetNameForAction(Actions.PRIMARY),
            KeyCodesDictionary.GetNameForAction(Actions.SECONDARY),
            KeyCodesDictionary.GetNameForAction(Actions.PAUSE)
        );
    }

    private void LoadNewScene(string scene)
    {
        if (Operation != null)
            return;
        Operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void OnClickBack() => LoadNewScene(Menus.MAIN);
}
