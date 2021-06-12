using UnityEngine;
using UnityEngine.Events;

public class EventsSystem : MonoBehaviour
{

    public UnityEvent OnCableDeleted;
    public UnityEvent OnCableCreated;
    public UnityEvent OnCableDisconnected;
    public UnityEvent OnCableConnected;
    public UnityEvent OnCableDropped;
    public UnityEvent OnCablePickedUp;
    public UnityEvent OnVictory;
    public UnityEvent<string, IEnable> OnEnterZone;
    public UnityEvent<string, IEnable> OnExitZone;
    public UnityEvent<string, float, float, float> OnNewMessage;
}
