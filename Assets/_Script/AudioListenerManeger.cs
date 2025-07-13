using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioListenerManager : MonoBehaviour
{
    void Awake()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        foreach (var listener in listeners)
        {
            // 自分自身はスキップ
            if (listener != GetComponent<AudioListener>())
            {
                Destroy(listener);
                Debug.Log("不要な AudioListener を削除しました → " + listener.gameObject.name);
            }
        }
    }
}