using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToGoGameScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
