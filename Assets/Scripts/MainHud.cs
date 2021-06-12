using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHud : MonoBehaviour
{
    public Text DistanceText;
    public Text LayoutText;
    public Text EndedText;
    public Text DebugText;
    public AInputManager inputManager;
    public CheckEnd checkEnd;

    float lastDistance = 0;
    float lastMaxDistance = 0;
    CableLayer cableLayer;
    string distanceFormat = "{0}{1}";
    string layoutFormat = "{0}";
    string endedFormat = "{0}";
    string debugFormat = "{0}";


    // Start is called before the first frame update
    void Start()
    {
        cableLayer = GetComponentInParent<CableLayer>();
        distanceFormat = DistanceText.text;
        layoutFormat = LayoutText.text;
        endedFormat = EndedText.text;
        debugFormat = DebugText.text;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Round(cableLayer.CurrentLength * 10) / 10;
        float maxDistance = Mathf.Round(cableLayer.MaxRange * 10) / 10;
        if (distance != lastDistance || maxDistance != lastMaxDistance)
            DistanceText.text = string.Format(distanceFormat, distance, maxDistance);
        LayoutText.text = string.Format(layoutFormat, inputManager.Layout.ToString());
        EndedText.text = string.Format(endedFormat, checkEnd.Ended ? "Yes" : "No");
        DebugText.text = string.Format(debugFormat, checkEnd.index);

    }
}
