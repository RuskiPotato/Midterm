
using UnityEngine;
 
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Attributes")]
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected float reachDistance = 0.5f;
 
    protected GameColor enemyColor;
    protected Transform target;
 
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }
 
    public void Init(GameColor color, Transform playerTarget)
    {
        enemyColor = color;
        target = playerTarget;
        GetComponent<SpriteRenderer>().color = color.ToColor();
    }
 
    protected virtual void Update()
    {
        if (target == null) return;
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;
 
        Move();
 
        if (Vector2.Distance(transform.position, target.position) <= reachDistance)
        {
            PerformAttack();
        }
    }
 
    protected virtual void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
 
    public abstract void PerformAttack();
 
    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} took {damageAmount}");
    }
 
    public bool TakeDamage(GameColor bulletColor)
    {
        if (bulletColor != enemyColor) return false;
 
        TakeDamage(maxHealth);
        if (currentHealth <= 0f) Die();
        return true;
    }
 
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died :(");
        Destroy(gameObject);
    }
}
 