using UnityEngine;

public class UIPopupController : MonoBehaviour
{
    public GameObject[] allUIPanels;  // �\�������\���̂���SUI
    public GameObject targetUI;       // ����\��������UI

    public void ShowOnlyThisUI()
    {
        // �SUI����U��\����
        foreach (var panel in allUIPanels)
        {
            panel.SetActive(false);
        }

        // �ړI��UI�����\��
        targetUI.SetActive(true);
    }
}