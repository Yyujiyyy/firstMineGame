using UnityEngine;
using UnityEngine.InputSystem;

public class PressAnyButton : MonoBehaviour
{
    private InputAction _pressAnyKeyAction;

    private void OnEnable()
    {
        // �C�ӂ̃{�^�����͂����m����A�N�V�������쐬
        _pressAnyKeyAction = new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>", interactions: "Press");

        // ���͂��������Ƃ��̃C�x���g�n���h���o�^
        _pressAnyKeyAction.performed += OnAnyButtonPressed;

        // �A�N�V������L����
        _pressAnyKeyAction.Enable();
    }

    private void OnDisable()
    {
        // ���������C�x���g�����i���������[�N�h�~�j
        _pressAnyKeyAction.performed -= OnAnyButtonPressed;
        _pressAnyKeyAction.Disable();
    }

    private void OnAnyButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("�����̃{�^����������܂����I");
        // �����ɃX�^�[�g������V�[���J�ڂȂǂ��L�q
    }
}
