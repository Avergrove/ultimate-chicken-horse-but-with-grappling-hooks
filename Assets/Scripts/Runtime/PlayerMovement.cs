using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    AudioSource aSource;
    SpriteRenderer sr;
    Rigidbody2D rgbd;
    RopeSystem ropeSystem;
    public SmoothCamera mainCamera;

    // Grounded raycasts
    public ContactFilter2D groundedRaycastFilter;
    public RaycastHit2D[] hitResults = new RaycastHit2D[1];

    // Entity states
    public bool isGrounded;
    public bool isGrappled;
    public bool isDying;
    private Direction direction;

    // Movement stats
    public float maxGroundSpeed;
    [Tooltip("Speed lost in percent on every tick.")]
    public float xPercentSpeedDecay;
    public float xPercentAirSpeedDecay;
    public float jumpInitialSpeed;

    public int maxJumpCount;
    private int jumpCount;

    public float grappleJumpBoost;

    public float deathImpactMinSpeed;
    public float deathBaseSpeed;
    public float deathImpactMaxSpeed;

    // Look and feel
    public AudioClip jumpClip;
    public AudioClip fireClip;

    void Awake()
    {
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        ropeSystem = GetComponent<RopeSystem>();

        direction = Direction.Right;
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
        isGrappled = false;
        isGrounded = false;
        isDying = false;
    }

    // Update is called once per frame
    void Update()
    {
        groundCheck();
    }

    public void Move(float x, float y)
    {
        if (!isDying)
        {
            // Handle miscellaneous stuff not related directly to movement.
            // TODO: Avoid repeated calls by using event based calls.
            if (x > 0)
            {
                if (direction == Direction.Left)
                {
                    direction = Direction.Right;
                    this.OnDirectionChange(direction);
                }
            }

            else if (x < 0)
            {
                if (direction == Direction.Right)
                {
                    direction = Direction.Left;
                    this.OnDirectionChange(direction);
                }
            }




            // Actually handle movement
            if (isGrappled)
            {
                ropeSystem.Swing(x);
                ropeSystem.Rappel(y);
            }

            else
            {
                if (isGrounded)
                {
                    if (x > 0)
                    {
                        rgbd.velocity = new Vector2(maxGroundSpeed * x, rgbd.velocity.y);
                        anim.SetBool("isMoving", true);
                    }

                    else if (x < 0)
                    {
                        rgbd.velocity = new Vector2(maxGroundSpeed * x, rgbd.velocity.y);
                        anim.SetBool("isMoving", true);
                    }

                    else
                    {
                        rgbd.velocity = new Vector2(rgbd.velocity.x * (100 - xPercentSpeedDecay) / 100, rgbd.velocity.y);
                        anim.SetBool("isMoving", false);
                    }
                }

                else
                {
                    if (x > 0)
                    {
                        if (rgbd.velocity.x < maxGroundSpeed)
                        {
                            rgbd.velocity = new Vector2(maxGroundSpeed * x, rgbd.velocity.y);
                        }
                    }

                    else if (x < 0)
                    {
                        rgbd.velocity = new Vector2(maxGroundSpeed * x, rgbd.velocity.y);
                    }

                    else
                    {
                        // Speed decays slower in the air
                        rgbd.velocity = new Vector2(rgbd.velocity.x * (100 - xPercentAirSpeedDecay) / 100, rgbd.velocity.y);
                    }
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
                aSource.PlayOneShot(jumpClip, 0.25f);
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
            aSource.PlayOneShot(fireClip, 0.25f);
            jumpCount = maxJumpCount;
        }
    }

    // Releases the grappling hook
    public void ReleaseGrapplingHook(Vector2 cursorPosition)
    {
        ropeSystem.DetachRope();
        isGrappled = false;
    }

    /// <summary>
    /// Checks whether the player is on the ground
    /// TODO: Swap raycast check with collider check
    /// </summary>
    private void groundCheck()
    {

        int hits = Physics2D.Raycast(transform.position, Vector2.down, groundedRaycastFilter, hitResults, 1.1f);
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


    private void OnDirectionChange(Direction newDir)
    {
        if(newDir == Direction.Left)
        {
            sr.flipX = true;
        }

        else if(newDir == Direction.Right)
        {
            sr.flipX = false;
        }
    }

    /// <summary>
    /// Dying is technically a type of movement. For about 500 ms before you explode into confetti anyway.
    /// TODO: Implement a more satisfying "death".
    /// </summary>
    public void Die(Vector2 deathOrigin)
    {
        // 1) Be knocked back, this should scale and be clamped somewhat based on velocity on contact
        isDying = true;
        Vector2 dir = -(deathOrigin - (Vector2) this.transform.position).normalized;
        float deathSpeedParam = Mathf.InverseLerp(deathImpactMinSpeed, deathImpactMaxSpeed, rgbd.velocity.magnitude);
        
        this.rgbd.velocity = dir * Mathf.Lerp(deathBaseSpeed * 0.8f, deathBaseSpeed * 1.2f, deathSpeedParam);
        this.sr.color = Color.red;


        // 2) Be erased from existence, let's implement this later
        //  - Black out the screen by wiping according to death direction
        //  - After wipe, respawn.


        // 3) Respawn from checkpoint, stop dying. Perchance.
        StartCoroutine(Undie(0.5f));
    }

    private IEnumerator Undie(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.sr.color = Color.white;
        isDying = false;
    }

}
