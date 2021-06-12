using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnd : MonoBehaviour
{
    public float TargetCheckTime = 0.5f;
    public string NextScene;
    public bool Ended { private set; get; } = false;


    float targetElapsedTime = 0.1f;
    float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        Save.LastLevel = gameObject.scene.name;
    }

    public int Index { private set; get; } = 0;
    private bool allOk = true;


    void Update()
    {
        elapsed += Time.deltaTime;
        while (elapsed > targetElapsedTime)
        {
            CableConnector[] connectors = GetComponentsInChildren<CableConnector>();
            if (Index >= connectors.Length && connectors.Length > 0)
            {
                Ended = allOk;
                allOk = true;
                Index = 0;
                if (Ended)
                    SceneManager.LoadSceneAsync(NextScene, LoadSceneMode.Single);
            }
            if (Index < connectors.Length)
            {
                List<CableConnector> connected = connectors[Index].GetConnections();

                for (int j = Index + 1; j < connectors.Length; ++j)
                {
                    if (!connected.Contains(connectors[j]))
                    {
                        allOk = false;
                        break;
                    }
                }
                ++Index;
            }
            elapsed -= targetElapsedTime;
            targetElapsedTime = Mathf.Max(TargetCheckTime, 0.1f) / connectors.Length;
        }
    }
}
