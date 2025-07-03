using UnityEngine;
using TMPro;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private GameObject TextObject; // Button��Text�I�u�W�F�N�g���A�T�C��
    [SerializeField] private CountUpTimer countUpTimer;   // �^�C�}�[�X�N���v�g���A�T�C��

    private void Start()
    {
        // �ŏ��͔�\���ɂ��Ă���
        TextObject.SetActive(false);
    }

    public void ShowUI()
    {
        TextObject.SetActive(true);

        countUpTimer?.StartTimer();
    }

    public void HideUI()
    {
        TextObject?.SetActive(false);
    }
}