using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAction : MonoBehaviour, IPlayerEventHandler, IGrappleProjectileEventHandler, IMovingPlatformEventHandler
{
    AudioSource aSource;
    SpriteRenderer sr;
    Rigidbody2D rgbd;
    GrappleGun ropeSystem;

    // Related external GameObjects
    public GameObject spawnPoint;

    // Entity states
    private Player player;

    // Movement stats
    public float velocityPower;
    public float groundAccel;
    public float groundDecel;
    public float airAccel;
    public float airDecel;
    public float maxSpeed;
    public float maxAirSpeedDecel;
    public float jumpInitialSpeed;

    private Vector2 inheritedVelocity;

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
        aSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        ropeSystem = GetComponentInChildren<GrappleGun>();

        player = GetComponent<Player>();
        inheritedVelocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float x, float y)
    {
        if (!player.IsDying)
        {
            if (x != 0)
            {
                player.IsMoving = true;
                if (player.MovingDirection == Direction.Left && x > 0)
                {
                    player.MovingDirection = Direction.Right;
                }

                else if (player.MovingDirection == Direction.Right && x < 0)
                {
                    player.MovingDirection = Direction.Left;
                }
            }

            else
            {
                player.IsMoving = false;
            }


            // Actually handle movement
            if (player.IsGrappled)
            {
                ropeSystem.Swing(x);
                ropeSystem.Rappel(y);
            }

            else
            {
                float targetSpeed = maxSpeed * x;
                float speedDiff = targetSpeed - rgbd.velocity.x;
                float accelRate;
                float movement;
                if (player.IsGrounded)
                {
                    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? groundAccel : groundDecel;
                }

                else
                {
                    if (rgbd.velocity.x * targetSpeed > 0)
                    {
                        if (Mathf.Abs(rgbd.velocity.x) <= maxSpeed)
                        {
                            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? airAccel : airDecel;
                        }

                        else
                        {
                            accelRate = maxAirSpeedDecel * Mathf.Abs(1 - x);
                        }
                    }

                    else
                    {
                        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? airAccel : airDecel;
                    }

                }

                movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velocityPower) * Mathf.Sign(speedDiff);
                this.rgbd.AddForce(new Vector2(movement, 0));

            }

            ExecuteEvents.Execute<IPlayerEventHandler>(this.gameObject, null, (handler, data) => handler.OnMovingChange(player.IsMoving, player.MovingDirection));
        }
    }

    public void Jump()
    {
        if (jumpCount > 0)
        {
            if (!player.IsGrappled)
            {
                rgbd.velocity = new Vector2(rgbd.velocity.x, jumpInitialSpeed) + inheritedVelocity;
            }

            else
            {
                this.ReleaseGrapplingHook(Vector2.zero);
                rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y + grappleJumpBoost);
            }

            jumpCount--;
            ExecuteEvents.Execute<IPlayerEventHandler>(this.gameObject, null, (handler, data) => handler.OnJump());
        }
    }

    // Fires a grappling hook towards target position
    public void FireGrapplingHook(Vector2 cursorPosition)
    {
        ropeSystem.Fire(cursorPosition);
    }

    // Releases the grappling hook
    public void ReleaseGrapplingHook(Vector2 cursorPosition)
    {
        ropeSystem.DetachRope();
        player.IsGrappled = false;
    }

    /// <summary>
    /// Dying is technically a type of movement. For about 500 ms before you explode into confetti anyway.
    /// TODO: Implement a more satisfying "death".
    /// </summary>
    public void Die(Vector2 deathOrigin)
    {
        // 1) Be knocked back, this should scale and be clamped somewhat based on velocity on contact
        player.IsDying = true;
        Vector2 dir = -(deathOrigin - (Vector2)this.transform.position).normalized;
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
        player.transform.position = spawnPoint.transform.position;
        player.IsDying = false;
    }

    public void OnGroundedChange(bool isGrounded)
    {
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }
    }

    void IPlayerEventHandler.OnMovingChange(bool isMoving, Direction dir)
    {
        // Nothing happens.
    }

    void IPlayerEventHandler.OnGroundedChange(bool isGrounded)
    {
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }
    }

    void IPlayerEventHandler.OnJump()
    {
        // Nothing should happen, because this class itself is handling the logic.
    }

    void IPlayerEventHandler.OnGrappledChange(bool isGrappled)
    {
        // Nothing should happen, this class is already handling the logic for itself.
    }

    void IGrappleProjectileEventHandler.OnProjectileHit(bool isSuccess, Collider2D collider, Vector2 contactPoint)
    {
        player.IsGrappled = isSuccess;
        if (isSuccess)
        {
            aSource.PlayOneShot(fireClip, 0.25f);
            jumpCount = maxJumpCount;

        }
    }

    /// <summary>
    /// Inherit the velocity of the platform when leaving.
    /// </summary>
    /// <param name="platform"></param>
    void IMovingPlatformEventHandler.OnPlatformLeave(MovingPlatform platform)
    {
        inheritedVelocity = Vector2.zero;
    }

    void IMovingPlatformEventHandler.OnPlatformMove(MovingPlatform platform)
    {
        inheritedVelocity = platform.GetVelocity();
    }
}
