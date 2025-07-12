using UnityEngine;

public class SettingsProxy : MonoBehaviour
{
    public void ApplySettings()
    {
        if (PlayerControl.Instance != null)
        {
            // 実際の設定適用処理
            PlayerControl.Instance.UpdateDPIFromInput();
        }
        else
        {
            Debug.LogWarning("PlayerControlが存在しないため、設定は適用されませんでした（たぶんタイトルシーン）");
        }
    }
}