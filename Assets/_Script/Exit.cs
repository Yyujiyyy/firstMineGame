using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    public void ExitSetting()
    {
        popup.SetActive(false);
    }
}
