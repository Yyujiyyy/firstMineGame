using UnityEngine;
using TMPro;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private GameObject TextObject; // ButtonのTextオブジェクトをアサイン
    [SerializeField] private CountUpTimer countUpTimer;   // タイマースクリプトをアサイン
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private GameObject _R_enemy;
    [SerializeField] private GameObject[] _HideObj;

    private void Start()
    {
        // 最初は非表示にしておく
        TextObject.SetActive(false);
    }

    public void ShowUI()
    {
        TextObject.SetActive(true);

        countUpTimer?.StartTimer();

        //_HideObj に入っている全てのオブジェクトに対して、順番に obj という変数名で使えるようにする。
        foreach (GameObject obj in _HideObj)
        {
            obj.SetActive(false);
        }

        _Enemy.SetActive(false);

        _R_enemy.SetActive(true);
    }

    public void HideUI()
    {
        TextObject?.SetActive(false);
    }
}