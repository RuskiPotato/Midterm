using UnityEngine;

public class Grunt : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        maxHealth = 40f;
        currentHealth = maxHealth;
        moveSpeed = 1.5f;
    }
 
    public override void PerformAttack()
{
    Debug.Log("I hit player");
    GameManager.Instance.GameOver();
}

}
 