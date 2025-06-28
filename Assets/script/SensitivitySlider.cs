using TMPro;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SensitivityText;
    [SerializeField] private UnityEngine.UI.Slider sensitivitySlider; // �X���C�_�[�Q��
    [SerializeField] private TMP_InputField inputFieldSensitivity; // �� �ǉ��F���l���͗�

    public FPScamera cameraScript; // FPS�J�����X�N���v�g�Q��

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider.maxValue = 10.0f;         // �X���C�_�[�̏����ύX(10.0�ɕύX)

        // �ۑ����ꂽ�l���擾�B�Ȃ���΃f�t�H���g1.0f
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f);

        // �X���C�_�[�ƃe�L�X�g�ɔ��f
        sensitivitySlider.value = savedSensitivity;
        UpdateSensitivityDisplay(savedSensitivity);

        // �C�x���g���X�i�[�o�^
        sensitivitySlider.onValueChanged.AddListener(OnSpeedChanged);
        inputFieldSensitivity.onEndEdit.AddListener(OnInputFieldChanged);

        //SensitivityText.text = "Sensitivity : " + savedSensitivity.ToString("F2");
    }

    public void OnSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value); // ���x�ۑ�
        UpdateSensitivityDisplay(value);

        if (cameraScript != null)
        {
            cameraScript.Sensitivity = value; // �����ő����f
        }
    }

        private void OnInputFieldChanged(string input)
    {
        if (float.TryParse(input, out float value))
        {
            value = Mathf.Clamp(value, sensitivitySlider.minValue, sensitivitySlider.maxValue);
            sensitivitySlider.value = value; // �� ������OnSpeedChanged���Ă΂�܂�
        }
        else
        {
            // �����ȓ��͂Ȃ�X���C�_�[�̒l���ĕ\��
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
