using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform origin;   // Rayの開始位置
    public float length = 2f; // Rayの長さ
    public float speed = 20f;
    public float lifeTime = 1f;

    private LineRenderer lineRenderer;
    private Vector3 startPos;
    private Vector3 direction;
    private bool isFiring = false;
    private float timer = 0f;

    void Start()
    {
        if(origin == null)
        {
            origin = Camera.main.transform;
        }


        // LineRenderer追加
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 標準シェーダー
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2; // 2点で線
        lineRenderer.enabled = false;   //最初は非表示
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (isFiring)
        {
            startPos += direction * speed * Time.deltaTime;

            Vector3 endPos = startPos + direction * length;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);

            timer += Time.deltaTime;
            if(timer >= lifeTime)
            {
                lineRenderer.enabled = false;
                isFiring = false;   //完了
            }
        }
    }

    void Fire()
    {
        startPos = origin.position;
        direction = origin.forward.normalized;
        isFiring = true;
        timer = 0f;
        lineRenderer.enabled = true;
    }
}
