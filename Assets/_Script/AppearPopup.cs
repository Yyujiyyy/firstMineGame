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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            settingPopup.SetActive(true);
        }
    }

    public void AppearSettingPopup()        //void AppearSettingPopupではOnclickでアクセスできない！
    {
        settingPopup.SetActive(true);
    }
}
