using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float life = 5f;

    private GameColor BulletColor;

    public void Init(GameColor color)
    {
        BulletColor = color;
        GetComponent<SpriteRenderer>().color = color.ToColor();
        Destroy(gameObject, life);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(BulletColor);

            Destroy(gameObject);
        }
    }
}