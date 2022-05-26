using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    Rigidbody2D rgbd;
    RopeSystem ropeSystem;

    // Grounded raycasts
    public ContactFilter2D groundedRaycastFilter;
    public RaycastHit2D[] hitResults = new RaycastHit2D[1];

    // Entity states
    public bool isGrounded;
    public bool isGrappled;

    // Movement stats
    public float maxGroundSpeed;
    [Tooltip("Speed lost in percent on every tick.")]
    public float xPercentSpeedDecay;
    public float xPercentAirSpeedDecay;
    public float jumpInitialSpeed;

    public int maxJumpCount;
    private int jumpCount;

    public float grappleJumpBoost;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        ropeSystem = GetComponent<RopeSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
        isGrappled = false;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        groundCheck();
    }

    public void Move(Vector2 input)
    {
        if (isGrappled)
        {
            
        }

        else
        {
            if (isGrounded)
            {
                if (input.x > 0)
                {
                    sr.flipX = false;
                    rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                    anim.SetBool("isMoving", true);
                }

                else if (input.x < 0)
                {
                    sr.flipX = true;
                    rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                    anim.SetBool("isMoving", true);
                }

                else
                {
                    rgbd.velocity = new Vector2(rgbd.velocity.x * (100 -  xPercentSpeedDecay) / 100, rgbd.velocity.y);
                    anim.SetBool("isMoving", false);
                }
            }

            else
            {
                if (input.x > 0)
                {
                    sr.flipX = false;
                    if (rgbd.velocity.x < maxGroundSpeed)
                    {
                        rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                    }
                }

                else if (input.x < 0)
                {
                    sr.flipX = true;
                    rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                }

                else
                {
                    // Speed decays slower in the air
                    rgbd.velocity = new Vector2(rgbd.velocity.x * (100 - xPercentAirSpeedDecay) / 100, rgbd.velocity.y);
                }
            }
        }
    }

    public void Jump()
    {
        if (!isGrappled)
        {
            if (jumpCount > 0)
            {
                rgbd.velocity = new Vector2(rgbd.velocity.x, jumpInitialSpeed);
                jumpCount--;
            }
        }

        else
        {
            this.ReleaseGrapplingHook(Vector2.zero);
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y + grappleJumpBoost);
            jumpCount--;
        }
    }

    // Fires a grappling hook towards target position, if successful, restock jump count.
    public void FireGrapplingHook(Vector2 cursorPosition)
    {
        isGrappled = ropeSystem.Fire(cursorPosition);
        if (isGrappled)
        {
            jumpCount = maxJumpCount;
        }
    }

    // Releases the grappling hook
    public void ReleaseGrapplingHook(Vector2 cursorPosition)
    {
        ropeSystem.DetachRope();
        isGrappled = false;
    }

    private void groundCheck()
    {

        int hits = Physics2D.Raycast(transform.position, Vector2.down, groundedRaycastFilter, hitResults, 2);
        if (hits > 0)
        {
            if (!isGrounded)
            {
                isGrounded = true;
                anim.SetBool("isGrounded", isGrounded);
                jumpCount = maxJumpCount;
            }
        }

        else
        {
            if (isGrounded)
            {
                isGrounded = false;
                anim.SetBool("isGrounded", isGrounded);
            }
        }
    }

}
