using UnityEngine;
 
public class Speeder : Enemy //fast
{
    protected override void Awake()
    {
        base.Awake();
        maxHealth = 15f;
        currentHealth = maxHealth;
        moveSpeed = 3f;
    }
 
   public override void PerformAttack()
{
    Debug.Log("I dropkick player");
    GameManager.Instance.GameOver();
}
}
 