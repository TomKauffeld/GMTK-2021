using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_1 : MonoBehaviour
{
    public string NextLevel;


    EventsSystem eventsSystem;

    void Start()
    {
        eventsSystem = FindObjectOfType<EventsSystem>();
        StartCoroutine(Startup());
        eventsSystem.OnCableCreated.AddListener(OnCableCreated);
        eventsSystem.OnEnterZone.AddListener(OnEnterZone);
        eventsSystem.OnVictory.AddListener(OnVictory);
    }

    IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Welcome to our game", 1, 3, 2);
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Walk towards one of the houses", 1, 3, 2);
    }

    void OnEnterZone(string zone, IEnable enable)
    {
        eventsSystem.OnNewMessage.Invoke("Try Clicking on the black circle", 1, 3, 2);
        enable.Enabled = false;
    }

    void OnCableCreated()
    {
        eventsSystem.OnNewMessage.Invoke("Now goto the other house", 1, 3, 2);
    }

   
    void OnVictory() => StartCoroutine(Closing());

    IEnumerator Closing()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Well Done :)", 1, 2, 2);
        yield return new WaitForSeconds(5);
        eventsSystem.OnNewMessage.Invoke("Up to the next Level", 1, 2, 2);
        yield return new WaitForSeconds(6);
        SceneManager.LoadSceneAsync(NextLevel, LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        eventsSystem.OnCableCreated.RemoveListener(OnCableCreated);
        eventsSystem.OnEnterZone.RemoveListener(OnEnterZone);
        eventsSystem.OnVictory.RemoveListener(OnVictory);
    }
}
