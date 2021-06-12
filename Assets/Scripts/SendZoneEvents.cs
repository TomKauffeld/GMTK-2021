using UnityEngine;

public class SendZoneEvents : MonoBehaviour, IEnable
{
    public string Zone;
    public bool Enabled { get; set; } = true;

    EventsSystem eventsSystem;
    void Start()
    {
        eventsSystem = FindObjectOfType<EventsSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Enabled && other != null && other.CompareTag("Player"))
            eventsSystem.OnEnterZone.Invoke(Zone, this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (Enabled && other != null && other.CompareTag("Player"))
            eventsSystem.OnExitZone.Invoke(Zone, this);
    }
}
