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
            settingPopup.SetActive(!settingPopup.activeSelf);  // true → false / false → true
            //settingPopup.activeSelf は、現在アクティブかどうか（表示中かどうか）を返す。
            //!settingPopup.activeSelf とすることで、現在の状態を反転して設定できる（トグル処理）。
        }
    }

    public void AppearSettingPopup()        //void AppearSettingPopupではOnclickでアクセスできない！
    {
        settingPopup.SetActive(true);
    }
}
