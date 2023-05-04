using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Transform GroundChercker;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private int lives = 3; 

    private SpriteRenderer Sprite;
    private Rigidbody2D RB;
    private Animator AnimController;
    private bool isGround = false;
    
    public static Player Instance { get; set; }
    private States State
    {
        get { return (States)AnimController.GetInteger("State"); }
        set { AnimController.SetInteger("State", (int)value); }
    }

    void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        AnimController = GetComponent<Animator>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        
        if(Input.GetButton("Horizontal"))
            Run();
        else
        {
            if (isGround) State = States.Idle;
        }
            
        if (Input.GetButton("Jump") && isGround)
            Jump();
    }

    private void Run()
    {
        if (isGround) State = States.Run;
        float dir = Input.GetAxis("Horizontal") * speed;
        Sprite.flipX = dir < 0f;
        RB.velocity = new Vector2(dir, RB.velocity.y);
    }
    private void Jump()
    {
        RB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        State = States.Jump;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundChercker.position, 0.2f);
        isGround = colliders.Length > 1;

        if (!isGround && RB.velocity.y < 0) State = States.Fall; 
    }

    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log("Lives: " + lives);
        if (lives < 1)
            Die();
    }

}


enum States
{
    Idle,
    Run,
    Jump,
    Fall
}