using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_4 : ALevel
{

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Here we also have \"Routers\"", 1, 3, 2);
        eventsSystem.OnNewMessage.Invoke("They have multiple connections available", 1, 3, 2);
    }

    protected override IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Well Done :)", 1, 2, 2);
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(NextLevel, LoadSceneMode.Single);
    }
}
