using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundState {GROUNDED, JUMPING, FALLING }
public class RigidboydMovement : MonoBehaviour
{
    [SerializeField] private LayerMask EnvironmentMask;

    /// <summary>
    /// This affects the gravity only when falling
    /// </summary>
    [SerializeField] private float GravityMulitplier;

    /// <summary>
    /// Affects how fast Gravity accelerates when falling
    /// </summary>
    [SerializeField] private float GravityAccel;

    /// <summary>
    /// Caps how fast Player can move vertically to prevent large velocity amounts
    /// </summary>
    ///
    [Header("Vertical Velocities")]
    [Tooltip("Caps how fast the player can move Up or Down")]
    [SerializeField] private float MaxVerticalVelocity;
    [Tooltip("Maximum gravity increase when Falling")]
    [SerializeField] private float MaxGravityFallMultiplier;

    [Header("Hanging Velocity Threshold")]
    [Tooltip("The threshold that the Y-velocity must be between for the character to temporarily hang in the air.\n Min > Y-velocity > Max")]
    [SerializeField] private float MinHangThreshold;
    [SerializeField] private float MaxHangThreshold;

    [Header("Groundcheck")]
    [Tooltip("Place a transform at the 'feet' and refernce it here")]
    [SerializeField] private Transform GroundCheckTransform;
    private const float GroundDetectRadius = 0.01f;
    [SerializeField] private bool Grounded;

    private Rigidbody2D myBody;

    private Vector2 Horizontal;
    [SerializeField] private Vector2 Vertical;

    [Header("Attributes")]
    [Tooltip("Sets horizontal speed of the character")]
    [SerializeField] private float Speed;
    private float InitSpeed;
    [Tooltip("Sets MAX jumpheight of the character. Corresponds with transform units (ie. 10 means max jumpheight will allow current position + 10 units).")]
    [SerializeField] private float MaxJumpHeight;

    private GroundState groundState;

    private bool JumpCalled;
    private bool GroundCheckEnabled;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();

        myBody.gravityScale = 1.0f;

        Horizontal = Vector2.zero;
        Vertical = Vector2.zero;

        groundState = GroundState.FALLING;
        GroundCheckEnabled = true;

        InitSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {

        switch (groundState)
        {
            case GroundState.GROUNDED:
                Vertical = Vector2.zero;
                GravityMulitplier = 0.0f;
                Speed = InitSpeed;

                if (GroundCheckEnabled && !GroundCheck())
                {
                    groundState = GroundState.FALLING;
                }
                else if (JumpCalled)
                {
                    StartCoroutine(DisableGroundCheck(0.2f));
                    float jumpForce = Mathf.Sqrt(MaxJumpHeight * Physics2D.gravity.y * -2);
                    Vertical.y = jumpForce;
                    groundState = GroundState.JUMPING;
                }

                break;
            case GroundState.JUMPING:
                JumpCalled = false;
                GravityMulitplier = 1.0f;

                //Allows player to hand for a bit when going from upwards to downwards
                if (myBody.velocity.y > MinHangThreshold && myBody.velocity.y < MaxHangThreshold)
                {
                    GravityMulitplier -= Time.fixedDeltaTime;
                    GravityMulitplier = Mathf.Clamp(GravityMulitplier, 0.05f, 10);
                }
                else if (myBody.velocity.y <= -0.2f)
                {
                    groundState = GroundState.FALLING;
                }

                break;
            case GroundState.FALLING:
                GravityMulitplier += 0.5f;
                GravityMulitplier = Mathf.Clamp(GravityMulitplier, -1, MaxGravityFallMultiplier);

                if (GroundCheckEnabled && GroundCheck())
                {
                    groundState = GroundState.GROUNDED;
                }

                break;
            default:
                groundState = GroundState.GROUNDED;
                break;
        }

        Vertical += GravityMulitplier * Physics2D.gravity * Time.deltaTime;
        Vertical.y = Mathf.Clamp(Vertical.y, -MaxVerticalVelocity, MaxVerticalVelocity);

        myBody.velocity = (Horizontal*Speed) + (Vertical);
        
        
        //Vector2 currentVelocity = myBody.velocity;
        //Debug.Log(groundState+" |" + currentVelocity);
    }
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Horizontal = -Vector2.right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Horizontal = Vector2.right;
        }
        else
        {
            Horizontal = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (groundState == GroundState.GROUNDED)
            {
                JumpCalled = true;
            }            
        }

        //Variable jump
        //Cuts the vertical velocity when jump is let go early
        if (Input.GetKeyUp(KeyCode.Space) && Vertical.y> 0.0f)
        {
            Vertical.y *= 0.0f;
        }
    }
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundDetectRadius, EnvironmentMask);
    }
    
    private IEnumerator DisableGroundCheck(float time)
    {
        GroundCheckEnabled = false;
        float t = 0.0f;
        while (t < time) 
        {
            t += Time.deltaTime;
            yield return null;
        }
        GroundCheckEnabled = true;

        yield return null;
    }
}
