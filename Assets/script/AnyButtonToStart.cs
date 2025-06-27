using UnityEngine;
using TMPro;

public class AnyButtonToStart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText; // 透過させたいText
    [SerializeField] private float speed = 1f;   // 点滅の速さ
    [SerializeField] private float minAlpha = 0.2f; // 最小透明度
    [SerializeField] private float maxAlpha = 1.0f; // 最大透明度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 時間経過によるsin値の取得（-1〜1）
        float sinValue = Mathf.Sin(Time.time * speed);

        // -1～1 を 0～1 に変換 → minAlpha〜maxAlpha にスケーリング
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (sinValue + 1f) / 2f);

        // アルファ値の変更
        if (targetText != null)
        {
            Color color = targetText.color;
            color.a = alpha;
            targetText.color = color;
        }
    }
}
