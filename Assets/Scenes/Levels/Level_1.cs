using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_1 : ALevel
{
    public CanvasGroup Controls;

    protected override void Start()
    {
        base.Start();
        eventsSystem.OnCableCreated.AddListener(OnCableCreated);
        eventsSystem.OnEnterZone.AddListener(OnEnterZone);
        Controls.alpha = 1;
    }

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Welcome to our game", 0, 2, 1);
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Walk towards one of the houses", 0, 2, 1);
    }

    private void OnEnterZone(string zone, IEnable enable)
    {
        eventsSystem.OnNewMessage.Invoke("Try Clicking on the black circle", 0, 2, 1);
        enable.Enabled = false;
    }

    private void OnCableCreated()
    {
        eventsSystem.OnNewMessage.Invoke("Now goto the other house", 0, 2, 1);
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

    protected override void OnDestroy()
    {
        eventsSystem.OnCableCreated.RemoveListener(OnCableCreated);
        eventsSystem.OnEnterZone.RemoveListener(OnEnterZone);
        base.OnDestroy();
    }
}
