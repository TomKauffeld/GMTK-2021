using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_3 : ALevel
{

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Here there are 3 houses", 0, 2, 1);
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Connect each one to the switch in the middle", 0, 2, 1);
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
