using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasGroup))]
public class MessageController : MonoBehaviour
{
    public string Text;
    public float TotalTime => Timeout + FadeIn + FadeOut;

    public float Timeout = 2;
    public float FadeIn = 1;
    public float FadeOut = 1;


    Text textBox;
    float elapsedTime = 0;
    CanvasGroup canvasGroup;

    void Start()
    {
        textBox = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        textBox.text = Text;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > FadeIn)
        {
            if (elapsedTime > Timeout + FadeIn)
            {
                if (elapsedTime > Timeout + FadeOut + FadeIn)
                    Destroy(gameObject);
                else
                    canvasGroup.alpha = Mathf.Lerp(1, 0, (elapsedTime - Timeout - FadeIn) / FadeOut);
            }
        }
        else
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / FadeIn);
    }
}
