using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{

    // エクスポート（設定 → コード）
    public static string Export(CrosshairSetting s)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("0;s;1;P;"); // VALORANT風のヘッダー

        sb.Append($"c;{s.colorIndex};");
        sb.Append($"l;{s.thickness};");
        sb.Append($"o;{s.length};");
        sb.Append($"g;{s.gap};");
        sb.Append($"f;{(s.hasCenterDot ? 1 : 0)};");
        sb.Append($"a;{s.alpha};");
        sb.Append($"h;{(s.outline ? 1 : 0)};");
        sb.Append($"b;{(s.useOuterLines ? 1 : 0)};");
        sb.Append($"t;{s.outlineThickness};");
        sb.Append($"d;{(s.dynamicOffset ? 1 : 0)};");

        return sb.ToString();
    }
}

// クロスヘア設定クラス
[System.Serializable]
public class CrosshairSetting
{
    public int colorIndex = 5;          // VALORANTの c;5
    public float thickness = 2f;        // l;2
    public float length = 4f;           // o;4
    public float gap = 2f;              // g;2
    public bool hasCenterDot = false;   // f;0 or f;1
    public float alpha = 1f;            // a;1
    public bool outline = false;        // h;0 or h;1
    public float outlineThickness = 1f; // t;1
    public bool useOuterLines = false;  // b;0 or b;1
    public bool dynamicOffset = false;  // d;0 or d;1
}

// パーサー（コード → 設定）
// VALORANT のコード（例：0;s;1;P;c;5;h;0;0l;4;0o;2;0a;1;0f;0;1b;0）を読み込みます。
// キーごとに値をマッピングして CrosshairSetting に入れる仕組みです。
public static class ValorantCrosshairParser
{
    public static CrosshairSetting Parse(string code)
    {
        var setting = new CrosshairSetting();
        var tokens = code.Split(';');

        for (int i = 0; i < tokens.Length - 1; i++)
        {
            string key = tokens[i];
            string val = tokens[i + 1];

            switch (key)
            {
                case "c": // Color
                    setting.colorIndex = int.Parse(val);
                    break;
                case "l": // Line thickness
                    setting.thickness = float.Parse(val);
                    break;
                case "o": // Line length
                    setting.length = float.Parse(val);
                    break;
                case "g": // Gap
                    setting.gap = float.Parse(val);
                    break;
                case "f": // Dot
                    setting.hasCenterDot = val == "1";
                    break;
                case "a": // Alpha
                    setting.alpha = float.Parse(val);
                    break;
                case "h": // Outline
                    setting.outline = val == "1";
                    break;
                case "t": // Outline Thickness
                    setting.outlineThickness = float.Parse(val);
                    break;
                case "b": // Outer Lines
                    setting.useOuterLines = val == "1";
                    break;
                case "d": // Dynamic Offset
                    setting.dynamicOffset = val == "1";
                    break;
            }
        }
        return setting;
    }
}

// Unity UI に反映する
public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private RectTransform top, bottom, left, right;
    [SerializeField] private Image centerDot;

    // アウトラインのイメージ
    [SerializeField] private Image[] outlines;

    public void Apply(CrosshairSetting s)
    {
        // 色（VALORANTのカラープリセットを再現）
        Color color = ConvertColorIndex(s.colorIndex);
        color.a = s.alpha;

        top.GetComponent<Image>().color = color;
        bottom.GetComponent<Image>().color = color;
        left.GetComponent<Image>().color = color;
        right.GetComponent<Image>().color = color;
        centerDot.color = color;
        centerDot.gameObject.SetActive(s.hasCenterDot);

        // サイズと配置
        float halfGap = s.gap / 2f;

        top.sizeDelta = new Vector2(s.thickness, s.length);
        bottom.sizeDelta = new Vector2(s.thickness, s.length);
        left.sizeDelta = new Vector2(s.length, s.thickness);
        right.sizeDelta = new Vector2(s.length, s.thickness);

        top.anchoredPosition = new Vector2(0, halfGap + s.length / 2f);
        bottom.anchoredPosition = new Vector2(0, -(halfGap + s.length / 2f));
        left.anchoredPosition = new Vector2(-(halfGap + s.length / 2f), 0);
        right.anchoredPosition = new Vector2(halfGap + s.length / 2f, 0);

        // アウトライン
        if (outlines != null)
        {
            foreach (var outline in outlines)
            {
                outline.enabled = s.outline;
                outline.rectTransform.sizeDelta += Vector2.one * s.outlineThickness;
            }
        }

        // 外側ライン（VALORANT風：サブクロスヘア）
        if (s.useOuterLines)
        {
            // gapを大きめにしてコピーを出す処理を追加してもよい
            // → 実際には4本の外側線を別オブジェクトで管理するのが望ましい
        }

        // 動的オフセット（走る/撃つ時に揺れるイメージ）
        if (s.dynamicOffset)
        {
            float offset = Mathf.Sin(Time.time * 10f) * 2f;
            top.anchoredPosition += new Vector2(0, offset);
            bottom.anchoredPosition -= new Vector2(0, offset);
            left.anchoredPosition -= new Vector2(offset, 0);
            right.anchoredPosition += new Vector2(offset, 0);
        }
    }

    private Color ConvertColorIndex(int index)
    {
        return index switch
        {
            1 => Color.green,
            2 => Color.yellow,
            3 => Color.blue,
            4 => Color.cyan,
            5 => Color.white,
            6 => Color.red,
            _ => Color.white
        };
    }
}