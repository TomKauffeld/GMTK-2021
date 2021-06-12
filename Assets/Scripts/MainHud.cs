using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainHud : MonoBehaviour
{
    public bool Pause { get => Menu.activeSelf; set => Menu.SetActive(value); }
    public GameObject MessagePanel;

    private AsyncOperation Operation = null;

    public Text DistanceText;
    public Text LayoutText;
    public Text LevelText;
    public GameObject Menu;

    float lastDistance = 0;
    float lastMaxDistance = 0;
    CableLayer cableLayer;
    string distanceFormat = "{0}{1}";
    string layoutFormat = "{0}";
    string levelFormat = "{0}";
    private float timeRemaining = 0;

    readonly LinkedList<Tuple<string, float, float, float>> messages = new LinkedList<Tuple<string, float, float, float>>();


    // Start is called before the first frame update
    void Start()
    {
        cableLayer = GetComponentInParent<CableLayer>();
        distanceFormat = DistanceText.text;
        layoutFormat = LayoutText.text;
        levelFormat = LevelText.text;
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Round(cableLayer.CurrentLength * 10) / 10;
        float maxDistance = Mathf.Round(cableLayer.MaxRange * 10) / 10;
        if (distance != lastDistance || maxDistance != lastMaxDistance)
            DistanceText.text = string.Format(distanceFormat, distance, maxDistance);
        LayoutText.text = string.Format(layoutFormat, Settings.Layout.ToString());
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
        else if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;

    }

    public void SwitchLayout() => Settings.SwitchLayout();

    private void LoadNewScene(string scene)
    {
        if (Operation != null || scene == null)
            return;
        Operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }
    public void BackToMenu() => LoadNewScene(Menus.MAIN);




    public void AddMessage(string message, float fadein, float timeout, float fadeout)
    {
        messages.AddLast(new Tuple<string, float, float, float>(message, fadein, timeout, fadeout));
    }
}
