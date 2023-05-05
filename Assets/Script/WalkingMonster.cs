using UnityEngine;

public class WalkingMonster : Entity
{
    [SerializeField] private float speed = 3f;

    private Vector3 dir;
    private SpriteRenderer Sprite;

    private void Start()
    {
        dir = transform.right;
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 circlePosition = transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circlePosition, 0.1f);

        if (colliders.Length > 0) dir *= -1;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
        //Sprite.flipX = dir.x < 0f;
        transform.localScale = transform.right * dir.x + transform.up * transform.localScale.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
            Player.Instance.GetDamage();
    }
}
