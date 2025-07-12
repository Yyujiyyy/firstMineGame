using UnityEngine;

public class AppearPopup : MonoBehaviour
{
    [SerializeField] public GameObject settingPopup;        //設定画面
    bool _isOpen;

    // Start is called before the first frame update
    void Start()
    {
        settingPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 表示状態をトグル（true⇄false）する
            _isOpen = !_isOpen;
            settingPopup.SetActive(_isOpen);  
           // Debug.Log("Escape");
        }
    }

    public void AppearSettingPopup()        //void AppearSettingPopupでは"Onclick"でアクセスできない！
    {
        settingPopup.SetActive(true);

        // 状態フラグも true に更新しておくと、ESCキーの挙動と整合性がとれる
        _isOpen = true;
    }
}
