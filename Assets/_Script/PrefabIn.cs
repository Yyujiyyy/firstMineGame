using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabIn : MonoBehaviour
{
    public GameObject prefab;  // プレハブ参照
    public CheckBox checkBox; // CheckBox スクリプト参照

    void SomeFunction()
    {
        GameObject newObj = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);

        // 動的に _HideObj に追加するには List に変換して操作する
        var tempList = new List<GameObject>(checkBox._HideObj);
        tempList.Add(newObj);
        checkBox._HideObj = tempList.ToArray();  // 配列に戻す
    }
}


