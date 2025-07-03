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

    public void ShowUI()
    {
        buttonTextObject.SetActive(true);
    }

    public void HideUI()
    {
        buttonTextObject?.SetActive(false);
    }
}