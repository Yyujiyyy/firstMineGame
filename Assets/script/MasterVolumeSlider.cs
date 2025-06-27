using UnityEngine;
using UnityEngine.UI;


public class MasterVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider VolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        // �ۑ����ꂽ���ʂ�ǂݍ���Őݒ�i�Ȃ����1�j
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = savedVolume;
        VolumeSlider.value = savedVolume;

        // �X���C�_�[�̕ύX�C�x���g�Ɋ֐���o�^
        VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}
