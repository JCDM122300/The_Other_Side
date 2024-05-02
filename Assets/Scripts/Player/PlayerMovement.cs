using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player movement 

    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float MaxJumpHeight;
    private BoxCollider2D boxCollider;
    private bool IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    private Animator anim;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        //grab references from gameobject 
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //prevents player from falling over and rotating when they jump 
        anim = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        float horizontalInput = Input.GetAxis("Horizontal");
        //player walking left/right
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //flips player left/right
        if (horizontalInput > 0.01f)
        {
            //checks if player moves right
            transform.localScale = Vector3.one;

        }
        else if (horizontalInput < -0.01f)
        {
            //checks if player moves left
            transform.localScale = new Vector3(-1, 1, 1);

        }

        //Player jump
        if (Input.GetKey(KeyCode.Space) && IsGrounded == true)
        {
            Jump();
        }

        //set run anim parameters 
        anim.SetBool("Run", horizontalInput != 0);

        //adjust jump height in case 
        //if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        //{
        //rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y / 2);
        //}
    }
    private void Jump()
    {
        jumpPower = Mathf.Sqrt(MaxJumpHeight * Physics2D.gravity.y * -2);
        //controls jump speed
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);



    }
}
