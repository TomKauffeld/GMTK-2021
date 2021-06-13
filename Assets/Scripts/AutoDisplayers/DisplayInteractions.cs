using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayInteractions : MonoBehaviour
{
    private string interactionFormat;
    private Text box;


    void Start()
    {
        box = GetComponent<Text>();
        interactionFormat = box.text;
    }

    void Update()
    {
        box.text = string.Format(interactionFormat,
            KeyCodesDictionary.GetNameForAction(Actions.PRIMARY),
            KeyCodesDictionary.GetNameForAction(Actions.SECONDARY),
            KeyCodesDictionary.GetNameForAction(Actions.PAUSE)
        );
    }
}
