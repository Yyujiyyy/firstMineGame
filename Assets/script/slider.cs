using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Slider speedSlider; // スライダー参照

    // Start is called before the first frame update
    void Start()
    {
        speedSlider.maxValue = 10.0f;         // スライダーの上限を変更(10.0に変更)

        // 保存された値を取得。なければデフォルト3.0f
        float savedSpeed = PlayerPrefs.GetFloat("Speed", 3.0f);

        // スライダーとテキストに反映
        speedSlider.value = savedSpeed;
        speedText.text = "Sensitivity : " + savedSpeed.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FPScamera cameraScript; // FPSカメラスクリプト参照

    public void OnSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("Speed", value); // スピード保存
        speedText.text = "Sensitivity : " + value.ToString("F2");

        if (cameraScript != null)
        {
            cameraScript.speed = value; // ここで即反映
        }
    }
}
