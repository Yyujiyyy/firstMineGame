using UnityEngine;
using UnityEngine.InputSystem;

public class PressAnyButton : MonoBehaviour
{
    private InputAction _pressAnyKeyAction;

    private void OnEnable()
    {
        // 任意のボタン入力を検知するアクションを作成
        _pressAnyKeyAction = new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>", interactions: "Press");

        // 入力があったときのイベントハンドラ登録
        _pressAnyKeyAction.performed += OnAnyButtonPressed;

        // アクションを有効化
        _pressAnyKeyAction.Enable();
    }

    private void OnDisable()
    {
        // 無効化＆イベント解除（メモリリーク防止）
        _pressAnyKeyAction.performed -= OnAnyButtonPressed;
        _pressAnyKeyAction.Disable();
    }

    private void OnAnyButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("何かのボタンが押されました！");
        // ここにスタート処理やシーン遷移などを記述
    }
}
