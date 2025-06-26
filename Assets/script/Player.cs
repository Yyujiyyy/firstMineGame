using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private EnemyController _random;
    public ParticleSystem particle;   // Hierarchy上のParticleSystemを指定
    [SerializeField] public Transform enemy;
     Transform _tr;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("enemy").transform;

        _random = GameObject.FindObjectOfType<EnemyController>();
        //これでシーン内の最初に見つかった EnemyController を _random にセットできる。

        _tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //マウスを左クリックしたとき
        if (Input.GetMouseButtonDown(0))
        {
            //新しいRayを作る。
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                            　//Rayの発射地点               ,方向

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                                                    

                if (hitInfo.collider.CompareTag("enemy"))
                {
                    //particle.transform.position = hitInfo.point;       //particleの位置を当たった位置にする（ワープ先についてこない）
                    //instantiateするから必要ない↑
                    Instantiate(particle,hitInfo.point, Quaternion.identity);

                    if (_random != null)                    //nullチェック      解説
                    {
                        _random.EnemyGenerate();            //_random に入っているオブジェクトに対して EnemyGenerate() を呼び出す
                    }
                }
            }
        }
    }
}