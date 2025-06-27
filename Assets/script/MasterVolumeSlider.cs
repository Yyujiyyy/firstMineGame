using UnityEngine;
using UnityEngine.UI;


public class MasterVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider VolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        // 保存された音量を読み込んで設定（なければ1）
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = savedVolume;
        VolumeSlider.value = savedVolume;

        // スライダーの変更イベントに関数を登録
        VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}
