using UnityEngine;
using UnityEngine.SceneManagement;

public class Alpha1ToTitleGo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
            // Build Settingsで一番上のシーンをロードする
        }
    }
}
