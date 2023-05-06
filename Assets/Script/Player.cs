using UnityEngine;
using System.Collections.Generic;

public class Player : Entity
{
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
        Sprite = GetComponentInChildren<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        AnimController = GetComponentInChildren<Animator>();
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
            
        if (Input.GetButtonDown("Jump") && isGround)
            Jump();

        
    }

    private void Run()
    {
        if (isGround) State = States.Run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        Sprite.flipX = dir.x < 0f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
    }
    private void Jump()
    {
        RB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        State = States.Jump;
        isGround = false;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGround = colliders.Length > 1;
        //isGround = Physics2D.OverlapArea(top_left.position, bottom_right.position, ground_layers);
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