using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToTitleGo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            // Build Settings�ň�ԏ�̃V�[�������[�h����
        }
    }
}
