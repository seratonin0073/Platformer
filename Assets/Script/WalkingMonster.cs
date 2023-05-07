using UnityEngine;

public class WalkingMonster : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float patrolRadius = 5f;
    [SerializeField] private bool isAgressiveUnit = false;

    private Camera MainCamera;
    private Vector3 startPosition;
    private Vector3 dir;
    private SpriteRenderer Sprite;

    private void Start()
    {
        MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        startPosition = transform.position;
        dir = transform.right;
        Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {

    }

    void Patrol()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
            Player.Instance.GetDamage();
    }

    private void OnGUI()
    {
        //Vector3 screenPos = 
    }
}
