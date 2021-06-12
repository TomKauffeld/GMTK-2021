using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettingsController : MonoBehaviour
{
    public Text LayoutTextBox;

    private AsyncOperation Operation = null;

    private string layoutTextFormat = "{0}";

    void Start()
    {
        layoutTextFormat = LayoutTextBox.text;
    }

    void Update()
    {
        if (Operation != null && Operation.isDone)
            Operation = null;
        LayoutTextBox.text = string.Format(layoutTextFormat, Settings.Layout.ToString());
    }

    private void LoadNewScene(string scene)
    {
        if (Operation != null)
            return;
        Operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void OnClickBack() => LoadNewScene(Menus.MAIN);

    public void OnClickLayout() => Settings.SwitchLayout();
}
