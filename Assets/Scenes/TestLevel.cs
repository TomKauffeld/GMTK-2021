using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevel : ALevel
{
    protected override IEnumerator Closing()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(NextLevel, LoadSceneMode.Single);
    }

    protected override IEnumerator Startup()
    {
        yield return null;
    }

}
