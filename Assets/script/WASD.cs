using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    [SerializeField] private GameObject targetUI;

    // �{�^������Ăяo���p�̊֐�
    public void ShowUI()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
