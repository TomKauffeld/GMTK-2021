using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHud : MonoBehaviour
{
    public bool Pause { get => Menu.activeSelf; set => Menu.SetActive(value); }
    public GameObject MessagePanel;
    public GameObject CenterScreen;

    private AsyncOperation Operation = null;

    public Text DistanceText;
    public Text LevelText;
    public GameObject Menu;

    float lastDistance = 0;
    float lastMaxDistance = 0;
    CableLayer cableLayer;
    string distanceFormat = "{0}{1}";
    string levelFormat = "{0}";
    private float timeRemaining = 0;

    readonly LinkedList<Tuple<string, float, float, float>> messages = new LinkedList<Tuple<string, float, float, float>>();


    // Start is called before the first frame update
    void Start()
    {
        cableLayer = GetComponentInParent<CableLayer>();
        distanceFormat = DistanceText.text;
        levelFormat = LevelText.text;
        Menu.SetActive(false);
        FindObjectOfType<EventsSystem>().OnNewMessage.AddListener(AddMessage);
    }

    // Update is called once per frame
    void Update()
    {
        CenterScreen.SetActive(!Pause);
        float distance = Mathf.Round(cableLayer.CurrentLength * 10) / 10;
        float maxDistance = Mathf.Round(cableLayer.MaxRange * 10) / 10;
        if (distance != lastDistance || maxDistance != lastMaxDistance)
            DistanceText.text = string.Format(distanceFormat, distance, maxDistance);
        LevelText.text = string.Format(levelFormat, gameObject.scene.name);



        if (Operation != null && Operation.isDone)
            Operation = null;


        if (messages.First != null && timeRemaining <= 0)
        {
            Tuple<string, float, float, float> message = messages.First.Value;
            messages.RemoveFirst();


            GameObject gameObject = Instantiate(MessagePanel, transform);
            MessageController controller = gameObject.GetComponent<MessageController>();


            controller.Text = message.Item1;
            controller.FadeIn = message.Item2;
            controller.Timeout = message.Item3;
            controller.FadeOut = message.Item4;


            timeRemaining = controller.TotalTime * 1.1f;
        }
        else if (timeRemaining > 0 && !Pause)
            timeRemaining -= Time.deltaTime;

    }


    public void OnClickContinue() => Pause = false;




    public void AddMessage(string message, float fadein, float timeout, float fadeout)
    {
        messages.AddLast(new Tuple<string, float, float, float>(message, fadein, timeout, fadeout));
    }

    private void OnDestroy()
    {
        EventsSystem eventsSystem = FindObjectOfType<EventsSystem>();
        if (eventsSystem != null)
            eventsSystem.OnNewMessage.RemoveListener(AddMessage);
    }
}
