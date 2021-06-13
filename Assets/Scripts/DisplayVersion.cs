using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayVersion : MonoBehaviour
{
    
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = Application.version + "u" + Application.unityVersion;
    }
}
