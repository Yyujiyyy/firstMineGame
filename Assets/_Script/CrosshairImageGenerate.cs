using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairImageGenerate : MonoBehaviour
{
    [Header("クロスヘア設定")]
    public float lineLength = 20f;     // 線の長さ
    public float lineThickness = 2f;   // 線の太さ
    public float gap = 5f;             // 中心からの距離
    public Color color = Color.white;   // 線の色

    // 子オブジェクト（線）を保持
    private RectTransform top, bottom, left, right;

    void Start()
    {
        CreateLine(ref top, "Top", new Vector2(lineThickness, lineLength));
        CreateLine(ref bottom, "Bottom", new Vector2(lineThickness, lineLength));
        CreateLine(ref left, "Left", new Vector2(lineLength, lineThickness));
        CreateLine(ref right, "Right", new Vector2(lineLength, lineThickness));

        PositionLines();
    }

    // Image オブジェクトを生成
    private void CreateLine(ref RectTransform rect, string name, Vector2 size)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);
        go.transform.localScale = Vector3.one;

        rect = go.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.localPosition = Vector3.zero;

        Image img = go.AddComponent<Image>();
        img.color = color;
    }

    // 上下左右の位置を設定
    private void PositionLines()
    {
        float halfGap = gap;

        top.anchoredPosition = new Vector2(0, halfGap + lineLength / 2f);
        bottom.anchoredPosition = new Vector2(0, -(halfGap + lineLength / 2f));
        left.anchoredPosition = new Vector2(-(halfGap + lineLength / 2f), 0);
        right.anchoredPosition = new Vector2(halfGap + lineLength / 2f, 0);
    }

    // Update で色や位置を更新したい場合もここに処理追加可能
}
