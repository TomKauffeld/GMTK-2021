using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_2 : ALevel
{
    public GameObject Switch;

    private enum StateEnum
    {
        CREATE_CABLE,
        GOTO_HOUSE,
        CONNECT_SWITCH,
        CREATE_ON_SWITCH,
        CONNECT_TO_HOUSE
    }

    private StateEnum State = StateEnum.CREATE_CABLE;

    protected override void Start()
    {
        base.Start();
        eventsSystem.OnCableCreated.AddListener(OnCableCreated);
        eventsSystem.OnCableTooLong.AddListener(OnCableTooLong);
        Switch.SetActive(false);
    }

    protected override IEnumerator Startup()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("This is the same as last time", 1, 3, 2);
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("Walk towards one of the houses", 1, 3, 2);
    }

    private void OnCableCreated()
    {
        if (State == StateEnum.CREATE_CABLE)
        {
            eventsSystem.OnNewMessage.Invoke("Now goto the other house", 1, 3, 2);
            State = StateEnum.GOTO_HOUSE;
        }
        else if (State == StateEnum.CREATE_ON_SWITCH)
        {
            eventsSystem.OnNewMessage.Invoke("Now you can connect the other house", 1, 3, 2);
            State = StateEnum.CONNECT_TO_HOUSE;
        }
    }

    private void OnCableTooLong()
    {
        if (State == StateEnum.GOTO_HOUSE)
        {
            State = StateEnum.CONNECT_SWITCH;
            StartCoroutine(CreateSwitch());
        }
    }

    private IEnumerator CreateSwitch()
    {
        yield return new WaitForSeconds(0.5f);
        eventsSystem.OnNewMessage.Invoke("As you see, sometimes the cable is red", 1, 2, 0.5f);
        eventsSystem.OnNewMessage.Invoke("This means the cable is too long", 0.5f, 1, 0.5f);
        eventsSystem.OnNewMessage.Invoke("For this you can use a switch", 0.5f, 1, 0.5f);
        eventsSystem.OnNewMessage.Invoke("Like the one that just spawned", 0.5f, 1, 0.5f);
        yield return new WaitForSeconds(8);
        Switch.SetActive(true);

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
        eventsSystem.OnCableTooLong.RemoveListener(OnCableTooLong);
        eventsSystem.OnCableCreated.RemoveListener(OnCableCreated);
        base.OnDestroy();
    }
}
