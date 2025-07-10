using TMPro;
using UnityEngine;

public class CountDown50 : MonoBehaviour
{
    public float currentCount = 50f;
    public TextMeshProUGUI countdownText;

    [SerializeField] private CountUpTimer countUpTimer; // ← CountUpTimer を参照

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    public void DocumentCount()
    {
        if(currentCount > 0)
        {
            currentCount -= 1;
            UpdateText();
        }

        if (currentCount <= 0)
        {
            countUpTimer?.StopTimer(); // ← ここで止める
        }
    }   

    private void UpdateText()
    {
        if (countdownText != null)
            countdownText.text = Mathf.Ceil(currentCount).ToString();
    }

    // SetActive(true) のたびに呼ばれる
    void OnEnable()
    {
        currentCount = 50f;     // ← カウントリセット
        UpdateText();           // ← 表示更新
    }
}
