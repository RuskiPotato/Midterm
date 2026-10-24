using UnityEngine;

public class Player : MonoBehaviour, ISwitch
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 1f;

    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private LayerMask enemyLayer = ~0;

    [SerializeField] private GameColor currentColor = GameColor.Red;

    private SpriteRenderer sr;
    private float timer;

    public GameColor CurrentColor => currentColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ApplyColor();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        Rotate2enemy();

        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            timer = 0f;
            Shoot();
        }
    }

    public void Switch()
    {
        currentColor = currentColor.Next();
        ApplyColor();
    }

    private void ApplyColor()
    {
        sr.color = currentColor.ToColor();
    }

    private Enemy FindEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

        Enemy nearest = null;
        float best = float.MaxValue;

        foreach (Collider2D h in hits)
        {
            if (!h.TryGetComponent(out Enemy enemy)) continue;

            float d = ((Vector2)h.transform.position - (Vector2)transform.position).sqrMagnitude;
            if (d < best)
            {
                best = d;
                nearest = enemy;
            }
        }
        return nearest;
    }

    private void Rotate2enemy()
    {
        Enemy target = FindEnemy();
        if (target == null) return;

        Vector2 dir = (Vector2)target.transform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        Transform origin = firePoint != null ? firePoint : transform;
        Bullet b = Instantiate(bulletPrefab, origin.position, transform.rotation);
        b.Init(currentColor);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        Switch();
    }
}