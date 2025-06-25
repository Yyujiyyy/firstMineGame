using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
            //PlayerPrefs.SetString("TitleText", "ゲーム中にESCで戻りました！");

            SceneManager.LoadScene(0);
            // Build Settingsで一番上のシーンをロードする
        }
    }
}
