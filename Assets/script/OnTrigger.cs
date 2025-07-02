using UnityEngine;
using UnityEngine.UI;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject buttonTextObject; // Button��Text�I�u�W�F�N�g���A�T�C��

    private void Start()
    {
        // �ŏ��͔�\���ɂ��Ă���
        buttonTextObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            buttonTextObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            buttonTextObject.SetActive(false);
        }
    }
}