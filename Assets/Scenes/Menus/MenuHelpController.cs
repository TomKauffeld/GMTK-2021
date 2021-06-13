using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHelpController : MonoBehaviour
{

    private AsyncOperation Operation = null;

    void Update()
    {
        if (Operation != null && Operation.isDone)
            Operation = null;
    }

    private void LoadNewScene(string scene)
    {
        if (Operation != null)
            return;
        Operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void OnClickBack() => LoadNewScene(Menus.MAIN);
}
