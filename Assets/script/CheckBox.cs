using UnityEngine;
using TMPro;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private GameObject TextObject; // Button��Text�I�u�W�F�N�g���A�T�C��
    [SerializeField] private CountUpTimer countUpTimer;   // �^�C�}�[�X�N���v�g���A�T�C��
    [SerializeField] private GameObject _Enemy;
    [SerializeField] private GameObject _R_enemy;
    [SerializeField] private GameObject[] _HideObj;

    private void Start()
    {
        // �ŏ��͔�\���ɂ��Ă���
        TextObject.SetActive(false);
    }

    public void ShowUI()
    {
        TextObject.SetActive(true);

        countUpTimer?.StartTimer();

        //_HideObj �ɓ����Ă���S�ẴI�u�W�F�N�g�ɑ΂��āA���Ԃ� obj �Ƃ����ϐ����Ŏg����悤�ɂ���B
        foreach (GameObject obj in _HideObj)
        {
            obj.SetActive(false);
        }

        _Enemy.SetActive(false);

        _R_enemy.SetActive(true);
    }

    public void HideUI()
    {
        TextObject?.SetActive(false);
    }
}