using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Slider speedSlider; // �X���C�_�[�Q��

    // Start is called before the first frame update
    void Start()
    {
        speedSlider.maxValue = 10.0f;         // �X���C�_�[�̏����ύX(10.0�ɕύX)

        // �ۑ����ꂽ�l���擾�B�Ȃ���΃f�t�H���g3.0f
        float savedSpeed = PlayerPrefs.GetFloat("Speed", 3.0f);

        // �X���C�_�[�ƃe�L�X�g�ɔ��f
        speedSlider.value = savedSpeed;
        speedText.text = "Sensitivity : " + savedSpeed.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FPScamera cameraScript; // FPS�J�����X�N���v�g�Q��

    public void OnSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("Speed", value); // �X�s�[�h�ۑ�
        speedText.text = "Sensitivity : " + value.ToString("F2");

        if (cameraScript != null)
        {
            cameraScript.speed = value; // �����ő����f
        }
    }
}
