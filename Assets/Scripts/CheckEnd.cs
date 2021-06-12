using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnd : MonoBehaviour
{
    public float TargetCheckTime = 0.5f;
    public bool Ended { private set; get; } = false;


    float targetElapsedTime = 0.1f;
    float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int index { private set; get; } = 0;
    private bool allOk = true;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        while (elapsed > targetElapsedTime)
        {
            CableConnector[] connectors = GetComponentsInChildren<CableConnector>();
            if (index >= connectors.Length && connectors.Length > 0)
            {
                Ended = allOk;
                allOk = true;
                index = 0;
            }
            if (index < connectors.Length)
            {
                List<CableConnector> connected = connectors[index].GetConnections();

                for (int j = index + 1; j < connectors.Length; ++j)
                {
                    if (!connected.Contains(connectors[j]))
                    {
                        allOk = false;
                        break;
                    }
                }
                ++index;
            }
            elapsed -= targetElapsedTime;
            targetElapsedTime = Mathf.Max(TargetCheckTime, 0.1f) / connectors.Length;
        }
    }
}
