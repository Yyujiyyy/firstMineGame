using TMPro;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SensitivityText;
    [SerializeField] private UnityEngine.UI.Slider sensitivitySlider; // スライダー参照
    [SerializeField] private TMP_InputField inputFieldSensitivity; // ← 追加：数値入力欄

    public FPScamera cameraScript; // FPSカメラスクリプト参照

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider.maxValue = 10.0f;         // スライダーの上限を変更(10.0に変更)

        // 保存された値を取得。なければデフォルト1.0f
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f);

        // スライダーとテキストに反映
        sensitivitySlider.value = savedSensitivity;
        UpdateSensitivityDisplay(savedSensitivity);

        // イベントリスナー登録
        sensitivitySlider.onValueChanged.AddListener(OnSpeedChanged);
        inputFieldSensitivity.onEndEdit.AddListener(OnInputFieldChanged);

        //SensitivityText.text = "Sensitivity : " + savedSensitivity.ToString("F2");
    }

    public void OnSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value); // 感度保存
        UpdateSensitivityDisplay(value);

        if (cameraScript != null)
        {
            cameraScript.Sensitivity = value; // ここで即反映
        }
    }

        private void OnInputFieldChanged(string input)
    {
        if (float.TryParse(input, out float value))
        {
            value = Mathf.Clamp(value, sensitivitySlider.minValue, sensitivitySlider.maxValue);
            sensitivitySlider.value = value; // ← 自動でOnSpeedChangedが呼ばれます
        }
        else
        {
            // 無効な入力ならスライダーの値を再表示
            inputFieldSensitivity.text = sensitivitySlider.value.ToString("F2");
        }
    }

    private void UpdateSensitivityDisplay(float value)
    {
        string display = value.ToString("F2");
        SensitivityText.text = "Sensitivity : " + display;
        inputFieldSensitivity.text = display;
    }
}
