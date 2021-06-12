using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{


    private AsyncOperation Operation = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Operation != null && Operation.isDone)
            Operation = null;
    }

    public void OnClickPlay()
    {
        if (Operation != null)
            return;
        Operation = SceneManager.LoadSceneAsync("Scenes/TestLevel", LoadSceneMode.Single);
    }
}
