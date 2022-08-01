using UnityEngine;

[RequireComponent(typeof(EnemyData))]
public class EnemyCollision : MonoBehaviour
{
    Enemy enemy;
    EnemyData enemyData;
    public Transform centerPoint;
    public float range = 1f;
    public LayerMask playerLayer;

    public Animator animator;


    private void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        enemy = enemyData.enemy;

    }

    public void TakeDamage(int damage)
    {
        enemyData.health -= damage;
        if (enemyData.name == "Boss(Clone)")
            enemyData.animator.SetTrigger("Hurt");
        if (enemyData.health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemyData.animator.SetBool("IsDead", true);
        if (enemyData.name == "Boss(Clone)")
            Delete();
        GetComponent<Collider2D>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }

    private void DropBubble()
    {
        float n = Random.Range(0f, 2f);
        if (n < 1)
            Instantiate(Resources.Load<GameObject>("Prefabs/Bubble"), transform.position, gameObject.transform.rotation);
    }

    private void DropCoin()
    {
        float n = Random.Range(0f, 2f);
        if (n < 1)
            Instantiate(Resources.Load<GameObject>("Prefabs/Coin"), transform.position, gameObject.transform.rotation);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}