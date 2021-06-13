using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_6 : ALevel
{

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("There are more and more houses", 0, 2, 1);
    }

    protected override IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Well Done :)", 0, 2, 1);
        yield return new WaitForSeconds(3);
        eventsSystem.OnNewMessage.Invoke("However there are no more levels :(", 0, 2, 1);
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(NextLevel, LoadSceneMode.Single);
    }
}
