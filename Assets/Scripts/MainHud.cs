using UnityEngine;
using UnityEngine.UI;

public class MainHud : MonoBehaviour
{
    public Text DistanceText;

    float lastDistance = 0;
    float lastMaxDistance = 0;
    CableLayer cableLayer;
    string distanceFormat = "{0}{1}";


    // Start is called before the first frame update
    void Start()
    {
        cableLayer = GetComponentInParent<CableLayer>();
        distanceFormat = DistanceText.text;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Round(cableLayer.CurrentLength * 10) / 10;
        float maxDistance = Mathf.Round(cableLayer.MaxRange * 10) / 10;
        if (distance != lastDistance || maxDistance != lastMaxDistance)
            DistanceText.text = string.Format(distanceFormat, distance, maxDistance);
    }
}
