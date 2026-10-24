using UnityEngine;
 

public abstract class Actor : MonoBehaviour
{

    [Header("Base Actor Attributes")]
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;
    [SerializeField] protected float moveSpeed = 3f;
 
 
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }
 
   
 
    public abstract void PerformAttack();
    
    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} took {damageAmount}");
    }
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died :(");
        Destroy(gameObject);
    }
}
 