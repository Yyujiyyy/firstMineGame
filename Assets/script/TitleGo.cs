using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //PlayerPrefs.SetString("TitleText", "�Q�[������ESC�Ŗ߂�܂����I");

            SceneManager.LoadScene(0);
            // Build Settings�ň�ԏ�̃V�[�������[�h����
        }
    }
}
