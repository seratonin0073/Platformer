using UnityEngine;

public class Worm : Entity
{
    [SerializeField] private int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.GetDamage();
            lives--;
            Debug.Log("WormLives: " + lives);
        }

        if (lives < 1)
            Die();
    }
}
