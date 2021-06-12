using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_2 : ALevel
{

    protected override void Start()
    {
        base.Start();
        eventsSystem.OnCableCreated.AddListener(OnCableCreated);
        eventsSystem.OnEnterZone.AddListener(OnEnterZone);
    }

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Welcome to our game", 1, 3, 2);
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Walk towards one of the houses", 1, 3, 2);
    }

    private void OnEnterZone(string zone, IEnable enable)
    {
        eventsSystem.OnNewMessage.Invoke("Try Clicking on the black circle", 1, 3, 2);
        enable.Enabled = false;
    }

    private void OnCableCreated()
    {
        eventsSystem.OnNewMessage.Invoke("Now goto the other house", 1, 3, 2);
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

    protected override void OnDestroy()
    {
        eventsSystem.OnCableCreated.RemoveListener(OnCableCreated);
        eventsSystem.OnEnterZone.RemoveListener(OnEnterZone);
        base.OnDestroy();
    }
}
