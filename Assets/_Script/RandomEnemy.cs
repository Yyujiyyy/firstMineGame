using UnityEngine;
using System.Collections;

public class RandomEnemy : MonoBehaviour
{
    //[SerializeField] private ParticleSystem deathEffect; // 死亡エフェクト
    [SerializeField][Range(1, 10)] private int maxHP = 4;


    private int currentHP;
    private bool isDead;

    private void Start()
    {
        Respawn(); // 初期スポーン時
    }

    public void TakeDamage(bool isHeadshot)
    {
        if (isDead) return;

        if (isHeadshot || --currentHP <= 0)
        {
            StartCoroutine(DieAndRespawn());
        }
    }

    public IEnumerator DieAndRespawn()
    {
        isDead = true;

        

        // しばらく待って再配置
        yield return new WaitForSeconds(0.01f);

        Respawn(); // ランダムワープして復活
    }

    private void Respawn()
    {
        float x = Random.Range(-20f, 20f);
        float z = Random.Range(1f, 20f);
        transform.position = new Vector3(x, 0.7f, z);

        currentHP = maxHP;
        isDead = false;
    }
}