using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button ButtonContinue;

    private AsyncOperation Operation = null;


    // Update is called once per frame
    void Update()
    {
        if (Operation != null && Operation.isDone)
            Operation = null;
        ButtonContinue.interactable = Save.LastLevel != null;
    }

    private void LoadNewScene(string scene)
    {
        if (Operation != null || scene == null)
            return;
        Operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void OnClickPlay() => LoadNewScene("Assets/Scenes/Levels/Level_1.unity");

    public void OnClickContinue() => LoadNewScene(Save.LastLevel);

    public void OnClickSettings() => LoadNewScene(Menus.SETTINGS);

    public void OnClickCredits() => LoadNewScene(Menus.CREDITS);

    public void OnClickHelp() => LoadNewScene(Menus.HELP);
}
