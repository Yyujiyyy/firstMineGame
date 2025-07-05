using UnityEngine;

public class UIPopupController : MonoBehaviour
{
    public GameObject[] allUIPanels;  // 表示される可能性のある全UI
    public GameObject targetUI;       // 今回表示したいUI

    public void ShowOnlyThisUI()
    {
        // 全UIを一旦非表示に
        foreach (var panel in allUIPanels)
        {
            panel.SetActive(false);
        }

        // 目的のUIだけ表示
        targetUI.SetActive(true);
    }
}