using TMPro;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SensitivityText;
    [SerializeField] private UnityEngine.UI.Slider sensitivitySlider; // スライダー参照

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider.maxValue = 10.0f;         // スライダーの上限を変更(10.0に変更)

        // 保存された値を取得。なければデフォルト1.0f
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f);

        // スライダーとテキストに反映
        sensitivitySlider.value = savedSensitivity;
        SensitivityText.text = "Sensitivity : " + savedSensitivity.ToString("F2");
    }

    public FPScamera cameraScript; // FPSカメラスクリプト参照

    public void OnSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value); // 感度保存
        SensitivityText.text = "Sensitivity : " + value.ToString("F2");     //小数点第３位を四捨五入して文字列に変換

        if (cameraScript != null)
        {
            cameraScript.Sensitivity = value; // ここで即反映
        }
    }
}
