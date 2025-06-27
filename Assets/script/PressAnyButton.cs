using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    private InputAction _pressAnyKeyAction;

    [SerializeField] private string sceneName;       // GameScene
    [SerializeField] private GameObject targetUI;    // ����������UI

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
        // �}�E�X�� targetUI ��ɂ��邩�𖈃t���[���m�F
        if (targetUI != null)
        {
            isPointerOverUI = RectTransformUtility.RectangleContainsScreenPoint
            (
                targetUI.GetComponent<RectTransform>(),
                Mouse.current.position.ReadValue(),
                null // Camera ���g���ꍇ�� Camera.main �ɂ���
            );
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