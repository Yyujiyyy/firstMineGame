using UnityEngine;
using UnityEngine.UI;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject buttonTextObject; // ButtonのTextオブジェクトをアサイン

    private void Start()
    {
        // 最初は非表示にしておく
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