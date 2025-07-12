using UnityEngine;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField dpiInputField;
    [SerializeField] private float valorantSensitivity = 0.4f; // 任意で設定

    public void ApplySettingsFromInput()
    {
        if (dpiInputField == null)
        {
            Debug.LogWarning("DPI入力欄が設定されていません");
            return;
        }

        if (float.TryParse(dpiInputField.text, out float dpi))
        {
            float unitySens = ConvertValorantToUnitySensitivity(valorantSensitivity, dpi);
            PlayerPrefs.SetFloat("Sensitivity", unitySens);
            PlayerPrefs.SetFloat("DPI", dpi);
            Debug.Log($"設定適用：DPI = {dpi}, 感度 = {unitySens}");
        }
        else
        {
            Debug.LogWarning("DPIの入力が不正です");
        }
    }

    private float ConvertValorantToUnitySensitivity(float valorantSens, float dpi)
    {
        return valorantSens * dpi * 0.000875f;
    }
}