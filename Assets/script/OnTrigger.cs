using UnityEngine;
using UnityEngine.UI;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject buttonTextObject; // ButtonのTextオブジェクトをアサイン
    [SerializeField] private CountUpTimer countUpTimer;   // タイマースクリプトをアサイン

    private void Start()
    {
        // 最初は非表示にしておく
        buttonTextObject.SetActive(false);
    }

    public void ShowUI()
    {
        buttonTextObject.SetActive(true);

        countUpTimer?.StartTimer();
    }

    public void HideUI()
    {
        buttonTextObject?.SetActive(false);
    }
}