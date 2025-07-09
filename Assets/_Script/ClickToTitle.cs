using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToTitle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
