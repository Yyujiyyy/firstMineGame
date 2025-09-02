using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCrosshairSetting : MonoBehaviour
{
    [SerializeField] GameObject CrosshairSettingPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GeneratePopup()
    {
        CrosshairSettingPopup.gameObject.SetActive(true);
    }
}
