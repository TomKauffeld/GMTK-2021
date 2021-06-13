using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettingsController : MonoBehaviour
{
    public Text LayoutTextBox;
    public Text InversePitchTextBox;
    public Text InverseYawTextBox;
    public Text SensitivityTextBox;
    public Slider SensitivitySlider;

    private AsyncOperation Operation = null;

    private string layoutTextFormat = "{0}";
    private string inversePitchTextFormat = "{0}";
    private string inverseYawTextFormat = "{0}";
    private string sensitivityTextFormat = "{0}";

    void Start()
    {
        layoutTextFormat = LayoutTextBox.text;
        inversePitchTextFormat = InversePitchTextBox.text;
        inverseYawTextFormat = InverseYawTextBox.text;
        sensitivityTextFormat = SensitivityTextBox.text;
        SensitivitySlider.value = Settings.Sensibility;
    }

    void Update()
    {
        if (Operation != null && Operation.isDone)
            Operation = null;
        LayoutTextBox.text = string.Format(layoutTextFormat, Settings.Layout.ToString());
        InversePitchTextBox.text = string.Format(inversePitchTextFormat, Settings.InversePitch ? "Yes" : "No");
        InverseYawTextBox.text = string.Format(inverseYawTextFormat, Settings.InverseYaw ? "Yes" : "No");
        SensitivityTextBox.text = string.Format(sensitivityTextFormat, Mathf.Round(Settings.Sensibility * 100) / 100);
    }

    private void LoadNewScene(string scene)
    {
        if (Operation != null)
            return;
        Operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void OnClickBack() => LoadNewScene(Menus.MAIN);

    public void OnClickLayout() => Settings.SwitchLayout();

    public void OnClickInversePitch() => Settings.InversePitch = !Settings.InversePitch;

    public void OnClickInverseYaw() => Settings.InverseYaw = !Settings.InverseYaw;

    public void OnValueSensitivity() => Settings.Sensibility = SensitivitySlider.value;
    //{
    //    float[] map = new float[] { 1 / 4f, 1/3f, 1/2f, 1f, 2f, 3f, 4f };
    //    Settings.Sensibility = map[(int)SensitivitySlider.value];
    //}
}
