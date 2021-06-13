using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_4 : ALevel
{

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Now we have 4 houses and 2 switches", 1, 3, 2);
    }

    protected override IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Well Done :)", 1, 2, 2);
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(NextLevel, LoadSceneMode.Single);
    }
}
