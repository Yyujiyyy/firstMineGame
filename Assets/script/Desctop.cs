using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
        // �G�f�B�^��ł͎~�܂�Ȃ��̂ŁA�ȉ��̃R�[�h�Œ�~��������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �r���h��͂���ŃA�v���P�[�V�������I���ł���i���f�X�N�g�b�v�ɖ߂�j
        Application.Quit();
#endif
    }
}