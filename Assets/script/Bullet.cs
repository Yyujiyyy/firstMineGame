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
    private bool isFiring = false;      //現在弾が発射中かどうかのフラグ。
    private float timer = 0f;

    void Start()
    {
        if(origin == null)
        {
            origin = Camera.main.transform;
        }

        // LineRenderer追加
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;     //端の太さ
        lineRenderer.endWidth = 0.1f;       //端の太さ
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); //線を表示するためのマテリアル（色を反映させるための設定）
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.positionCount = 2; // 2点で線     線を2点（始点と終点）で構成。
        lineRenderer.enabled = false;   //最初は非表示
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (isFiring)       //弾が現在発射中であれば、以下の処理を実行。
        {
            startPos += direction * speed * Time.deltaTime;     //startPosを計算       Time.deltaTime で毎秒移動に補正。
            //FPSに関係なく、1秒間で「speed」分進む。  speedのみだと高FPSで速く進みすぎる。低FPSだと遅くなる。

            Vector3 endPos = startPos + direction * length;     //endPosを計算     （始点からRayの長さぶん先）
            lineRenderer.SetPosition(0, startPos);              //計算結果を反映
            lineRenderer.SetPosition(1, endPos);                //計算結果を反映

            timer += Time.deltaTime;                            //経過時間を加算。  前のフレームから今のフレームまでにかかった時間
            if (timer >= lifeTime)
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
        timer = 0f;                                 //経過時間を加算。
        lineRenderer.enabled = true;                //線を表示開始。
        //setActiveはどのタイミングでも使える、子オブジェクトもまとめて無効、してしまう
    }
}
