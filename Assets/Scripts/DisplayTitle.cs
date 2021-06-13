using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayTitle : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Text>().text = Application.productName;
    }
}
