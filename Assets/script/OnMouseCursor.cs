using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseCursor : MonoBehaviour
{
    private RectTransform rectTransform;
    private Renderer objRenderer;

    // Start is called before the first frame update
    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //マウスがボタンの上にあるかどうか
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
        {
            
        }
    }
}
