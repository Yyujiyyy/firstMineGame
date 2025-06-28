using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    private InputAction _pressAnyKeyAction;

    [SerializeField] private string sceneName;       // GameScene
    [SerializeField] private GameObject[] targetUI;    // 無視したいUI

    private bool isPointerOverUI = false;

    private void Awake()
    {
        // 任意のボタン入力を検知するアクションを作成
        _pressAnyKeyAction = new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>", interactions: "Press");
        // 入力があったときのイベントハンドラ登録
        _pressAnyKeyAction.performed += OnAnyButtonPressed;
        _pressAnyKeyAction.Enable(); // 常に有効化（ただし実行はホバー中のみ）
    }

    private void Update()
    {
        isPointerOverUI = false;

        // マウスが targetUI 上にあるかを毎フレーム確認
        if (targetUI != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            foreach (GameObject uiElement in targetUI)
            {
                //アクティブなUIに限定
                if (uiElement != null && uiElement.activeInHierarchy)
                {
                    RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
                    if (rectTransform != null && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos, null))
                    {
                        isPointerOverUI = true;
                        break;
                    }
                }
            }
        }
    }

    private void OnAnyButtonPressed(InputAction.CallbackContext context)
    {
        if (!isPointerOverUI)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnDisable()
    {
        _pressAnyKeyAction.performed -= OnAnyButtonPressed;
        _pressAnyKeyAction.Disable();
    }
}