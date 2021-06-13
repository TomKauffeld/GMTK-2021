using System.Collections;
using UnityEngine;

public abstract class ALevel : MonoBehaviour
{
    public float MaxDistance = 30;
    public Transform CableParent = null;
    public string NextLevel;

    protected EventsSystem eventsSystem;

    protected virtual void Start()
    {
        Save.LastLevel = gameObject.scene.path;
        eventsSystem = FindObjectOfType<EventsSystem>();
        StartCoroutine(Startup());
        eventsSystem.OnVictory.AddListener(OnVictory);
    }

    private void OnVictory() => StartCoroutine(Closing());

    protected abstract IEnumerator Startup();
    protected abstract IEnumerator Closing();

    protected virtual void OnDestroy()
    {
        eventsSystem.OnVictory.RemoveListener(OnVictory);
    }
}
