using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_5 : ALevel
{

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("A little bit more difficult...", 0, 2, 1);
    }

    protected override IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Well Done :)", 0, 2, 1);
        yield return new WaitForSeconds(3);
        eventsSystem.OnNewMessage.Invoke("Up to the next Level", 0, 2, 1);
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(NextLevel, LoadSceneMode.Single);
    }
}
