using UnityEngine;

public class AppearPopup : MonoBehaviour
{
    [SerializeField] public GameObject settingPopup;        //�ݒ���

    // Start is called before the first frame update
    void Start()
    {
        settingPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppearSettingPopup()        //void AppearSettingPopup�ł�Onclick�ŃA�N�Z�X�ł��Ȃ��I
    {
        settingPopup.SetActive(true);
    }
}
