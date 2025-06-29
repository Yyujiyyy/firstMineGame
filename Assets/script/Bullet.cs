using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1000f;
    public float maxDistance = 100f;

    private Vector3 direction;
    private Vector3 startPos;

    public void Init(Vector3 origin, Vector3 target)
    {
        startPos = origin;
        direction = (target - origin).normalized;
        transform.position = origin;

        // Mesh‚ğ”ñ•\¦‚É‚·‚é‚È‚ç‚±‚±
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.enabled = false;
    }



    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            Destroy(gameObject); // ˆê’è‹——£’´‚¦‚½‚çíœ
        }
    }
}