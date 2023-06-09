using UnityEngine;

public class Ratcher : MonoBehaviour
{
    [SerializeField] private int lives = 2;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private int damage = 1;

    private void Update()
    {
        GetComponent<SpriteRenderer>().flipX = transform.position.x > Player.Instance.transform.position.x;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.GetDamage();
        }
    }
}
