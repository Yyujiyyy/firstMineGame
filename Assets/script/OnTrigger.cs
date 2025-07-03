using UnityEngine;
using UnityEngine.UI;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject buttonTextObject; // Button��Text�I�u�W�F�N�g���A�T�C��
    [SerializeField] private CountUpTimer countUpTimer;   // �^�C�}�[�X�N���v�g���A�T�C��

    private void Start()
    {
        // �ŏ��͔�\���ɂ��Ă���
        buttonTextObject.SetActive(false);
    }

    public void ShowUI()
    {
        buttonTextObject.SetActive(true);

        countUpTimer?.StartTimer();
    }

    public void HideUI()
    {
        buttonTextObject?.SetActive(false);
    }
}