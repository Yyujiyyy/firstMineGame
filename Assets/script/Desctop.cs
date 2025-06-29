using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
        // エディタ上では止まらないので、以下のコードで停止させられる
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後はこれでアプリケーションを終了できる（＝デスクトップに戻る）
        Application.Quit();
#endif
    }
}