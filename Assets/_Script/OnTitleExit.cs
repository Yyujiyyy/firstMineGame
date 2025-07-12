using UnityEngine;

public class OnTitleExit : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    public void ExitSetting()
    {
        popup.SetActive(false);
    }
}
