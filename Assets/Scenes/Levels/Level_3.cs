using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_3 : ALevel
{

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Here there are 3 houses", 1, 3, 2);
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Connect each one to the switch in the middle", 1, 3, 2);
    }

    protected override IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Well Done :)", 1, 2, 2);
        yield return new WaitForSeconds(5);
        eventsSystem.OnNewMessage.Invoke("Up to the next Level", 1, 2, 2);
        yield return new WaitForSeconds(6);
        SceneManager.LoadSceneAsync(NextLevel, LoadSceneMode.Single);
    }
}
