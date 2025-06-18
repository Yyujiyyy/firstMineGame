using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform origin;   // Ray�̊J�n�ʒu
    public float length = 2f; // Ray�̒���
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


        // LineRenderer�ǉ�
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �W���V�F�[�_�[
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2; // 2�_�Ő�
        lineRenderer.enabled = false;   //�ŏ��͔�\��
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
                isFiring = false;   //����
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
