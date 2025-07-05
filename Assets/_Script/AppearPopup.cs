using UnityEngine;

public class AppearPopup : MonoBehaviour
{
    [SerializeField] public GameObject settingPopup;        //設定画面

    // Start is called before the first frame update
    void Start()
    {
        settingPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppearSettingPopup()        //void AppearSettingPopupではOnclickでアクセスできない！
    {
        settingPopup.SetActive(true);
    }
}
