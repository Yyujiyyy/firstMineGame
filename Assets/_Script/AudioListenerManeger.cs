using UnityEngine;

//[RequireComponent(typeof(AudioListener))]

//[RequireComponent(typeof(AudioListener))] を付けると、Unity は そのスクリプトが存在する限り AudioListener を必ず付ける必要があると判断する。
//結果として、削除しようとしても自動で再追加される or 削除がブロックされるため、エラーが出る

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