using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("Speed", value); // スピード保存
    }
}
