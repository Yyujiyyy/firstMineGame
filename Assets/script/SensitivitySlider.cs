using TMPro;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SensitivityText;
    [SerializeField] private UnityEngine.UI.Slider sensitivitySlider; // �X���C�_�[�Q��

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider.maxValue = 10.0f;         // �X���C�_�[�̏����ύX(10.0�ɕύX)

        // �ۑ����ꂽ�l���擾�B�Ȃ���΃f�t�H���g1.0f
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f);

        // �X���C�_�[�ƃe�L�X�g�ɔ��f
        sensitivitySlider.value = savedSensitivity;
        SensitivityText.text = "Sensitivity : " + savedSensitivity.ToString("F2");
    }

    public FPScamera cameraScript; // FPS�J�����X�N���v�g�Q��

    public void OnSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value); // ���x�ۑ�
        SensitivityText.text = "Sensitivity : " + value.ToString("F2");     //�����_��R�ʂ��l�̌ܓ����ĕ�����ɕϊ�

        if (cameraScript != null)
        {
            cameraScript.Sensitivity = value; // �����ő����f
        }
    }
}
