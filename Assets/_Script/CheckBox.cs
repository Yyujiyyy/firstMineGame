using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CheckBox : MonoBehaviour
{
    //Text関連
    [SerializeField] private GameObject TextObject; // ButtonのTextオブジェクトをアサイン

    [SerializeField] private GameObject CountUptimerText;
    [SerializeField] private CountUpTimer countUpTimer;   // タイマースクリプトをアサイン

    [SerializeField] private GameObject CountDowntimer;

    //[SerializeField] private GameObject _Enemy;           // 通常の敵
    [SerializeField] private GameObject _T_enemy;         // 変化後の敵
    public GameObject[] _HideObj;       // 非表示にしたいオブジェクト群

    private bool isActive = false;   // UIが現在表示されているかどうかのフラグ

    private void Start()
    {
        // 最初は非表示にしておく
        TextObject?.SetActive(false);
        CountDowntimer.SetActive(false);
        CountUptimerText.SetActive(false);
        isActive = false;
    }

    // UI表示/非表示を切り替えるトグルメソッド（外部から呼び出す）
    public void ToggleUI()
    {
        //Debug.Log("ToggleUI called");
        if (isActive)
        {
            HideUI();   // 表示中だったら非表示に
            //Debug.Log("hide");
        }
        else
        {
            ShowUI();   // 非表示中だったら表示する
        }
    }

    public void ShowUI()
    {
        // UIを表示
        TextObject.SetActive(true);
        CountDowntimer.SetActive(true);
        CountUptimerText.SetActive(true);

        // タイマーを開始
        countUpTimer?.StartTimer();

        //_HideObj に入っている全てのオブジェクトに対して、順番に obj という変数名で使えるようにする。
        foreach (GameObject obj in _HideObj)
        {
            obj.SetActive(false); // 非表示にする
        }

        // 通常の敵を非表示にし、変化後の敵を表示
        //_Enemy.SetActive(false);
        _T_enemy.SetActive(true);

        // 表示状態を記録
        isActive = true;
    }

    public void HideUI()
    {
        // UIを非表示
        TextObject.SetActive(false);
        CountDowntimer.SetActive(false);
        CountUptimerText.SetActive(false);
        countUpTimer?.StopTimer();

        // _HideObj を再表示
        foreach (GameObject obj in _HideObj)
        {
            obj.SetActive(true); // 元に戻す
        }

        // 敵表示を元に戻す
        //_Enemy.SetActive(true);
        _T_enemy.SetActive(false);

        // 表示状態をリセット
        isActive = false;
    }

    // 実行時に要素を追加するメソッド
    public void AddToHideObj(GameObject obj)
    {
        // List に変換して追加してから配列に戻す
        List<GameObject> tempList = new List<GameObject>(_HideObj);
        tempList.Add(obj);
        _HideObj = tempList.ToArray();
    }
}