using UnityEngine;
using UnityEngine.SceneManagement;

public class F1ToTitleGo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
            // Build Settingsで一番上のシーンをロードする
        }
    }
}
