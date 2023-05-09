using UnityEngine;

public class WalkingMonster : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float positionOfPatrol = 3f;
    [SerializeField] private float stoppingDistance = 3f;
    [SerializeField] private float extraSpeed = 3f;

    private Vector2 startPoint;
    private Vector3 Direction;
    private SpriteRenderer Sprite;

    private bool isMovingRight = false;
    private bool isChill;
    private bool isAngry;
    private bool isGoBack;


    private void Awake()
    {
        startPoint = transform.position;
        Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, startPoint) < positionOfPatrol && isAngry == false)
        {
            isChill = true;
        }

        if(Vector2.Distance(transform.position, Player.Instance.transform.position) < stoppingDistance)
        {
            isAngry = true;
            isChill = false;
            isGoBack = false;
        }

        if(Vector2.Distance(transform.position, Player.Instance.transform.position) > stoppingDistance)
        {
            isGoBack = true;
            isAngry = false;
        }

        if(isChill)
        {
            Chill();
        }
        else if(isAngry)
        {
            Angry();
        }
        else if(isGoBack)
        {
            GoBack();
        }

        Sprite.flipX = !isMovingRight;
    }

    void Chill()
    {
        Debug.Log("Chill");
        if (transform.position.x > startPoint.x + positionOfPatrol)
        {
            isMovingRight = false;
        }
        else if(transform.position.x < startPoint.x - positionOfPatrol)
        {
            isMovingRight = true;
        }

        if(isMovingRight)
        {
            Direction = Vector2.right;
        }
        else
        {
            Direction = Vector2.left;
        }

        transform.position = Vector2.MoveTowards(transform.position, transform.position + Direction, Time.deltaTime * speed);

        
    }

    void Angry()
    {
        Debug.Log("Angry");
        transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * (speed + extraSpeed));
        isMovingRight = transform.position.x < Player.Instance.transform.position.x;
    }

    void GoBack()
    {
        Debug.Log("GoBack");
        transform.position = Vector2.MoveTowards(transform.position, startPoint, Time.deltaTime * speed);
        isMovingRight = transform.position.x < startPoint.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
            Player.Instance.GetDamage();
    }


}
