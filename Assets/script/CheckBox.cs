using UnityEngine;
using TMPro;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private GameObject TextObject; // ButtonのTextオブジェクトをアサイン
    [SerializeField] private CountUpTimer countUpTimer;   // タイマースクリプトをアサイン
    [SerializeField] private GameObject _enemy;

    private void Start()
    {
        // 最初は非表示にしておく
        TextObject.SetActive(false);
    }

    public void ShowUI()
    {
        TextObject.SetActive(true);

        countUpTimer?.StartTimer();

        _enemy.SetActive(false);
    }

    public void HideUI()
    {
        TextObject?.SetActive(false);
    }
}