using UnityEngine;

//[RequireComponent(typeof(AudioListener))]

//[RequireComponent(typeof(AudioListener))] を付けると、Unity は そのスクリプトが存在する限り AudioListener を必ず付ける必要があると判断する。
//結果として、削除しようとしても自動で再追加される or 削除がブロックされるため、エラーが出る

[RequireComponent(typeof(Camera), typeof(AudioListener))]
public class CameraWithAudio : MonoBehaviour
{
    private AudioListener listener;

    void Awake()
    {
        listener = GetComponent<AudioListener>();
    }

    void OnEnable()
    {
        listener.enabled = true;
    }

    void OnDisable()
    {
        listener.enabled = false;
    }
}