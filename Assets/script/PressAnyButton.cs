using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    private InputAction _pressAnyKeyAction;

    [SerializeField] private string sceneName;       // GameScene
    [SerializeField] private GameObject[] targetUI;    // ����������UI

    private bool isPointerOverUI = false;

    private void Awake()
    {
        // �C�ӂ̃{�^�����͂����m����A�N�V�������쐬
        _pressAnyKeyAction = new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>", interactions: "Press");
        // ���͂��������Ƃ��̃C�x���g�n���h���o�^
        _pressAnyKeyAction.performed += OnAnyButtonPressed;
        _pressAnyKeyAction.Enable(); // ��ɗL�����i���������s�̓z�o�[���̂݁j
    }

    private void Update()
    {
        isPointerOverUI = false;

        // �}�E�X�� targetUI ��ɂ��邩�𖈃t���[���m�F
        if (targetUI != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            foreach (GameObject uiElement in targetUI)
            {
                //�A�N�e�B�u��UI�Ɍ���
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