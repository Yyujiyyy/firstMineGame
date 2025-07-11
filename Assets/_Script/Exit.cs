using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private PlayerControl playerControl;       //PlayerControlのメソッドを使ってコンパクトにする

    public void ExitSetting()
    {
        popup.SetActive(false);
        playerControl.LockCursor(); // カーソルロック復帰
    }
}
