using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Describes the current state of the player.
/// </summary>
public class Player : MonoBehaviour
{
    // Related objects
    public GameObject spawnPoint;

    // Movement states
    private Direction movingDirection = Direction.Right;

    private bool isMoving = false;
    private bool isGrounded = false;
    private bool isGrappled = false;
    private bool isDying = false;

    // Grounded raycasts
    public ContactFilter2D groundedRaycastFilter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        groundCheck();
    }

    /// <summary>
    /// Checks whether the player is on the ground
    /// TODO: Swap raycast check with collider check
    /// </summary>
    private void groundCheck()
    {
        RaycastHit2D[] hitResults = new RaycastHit2D[1];

        int hits = Physics2D.Raycast(transform.position, Vector2.down, groundedRaycastFilter, hitResults, 1.1f);
        if (hits > 0)
        {
            if (!IsGrounded)
            {
                IsGrounded = true;
            }
        }

        else
        {
            if (isGrounded)
            {
                IsGrounded = false;
            }
        }
    }


    public Direction MovingDirection
    {
        get { return movingDirection; }
        set { movingDirection = value; }
    }

    public bool IsMoving
    {
        get { return isMoving; }
        set
        {
            isMoving = value;
            ExecuteEvents.Execute<IPlayerEventHandler>(this.gameObject, null, (playerEventHandler, eventData) => playerEventHandler.OnMovingChange(value, Direction.Right));
        }
    }

    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            isGrounded = value;
            ExecuteEvents.Execute<IPlayerEventHandler>(this.gameObject, null, (playerEventHandler, eventData) => playerEventHandler.OnGroundedChange(isGrounded));
        }
    }

    public bool IsGrappled
    {
        get { return isGrappled; }
        set
        {
            isGrappled = value;
        }
    }

    public bool IsDying
    {
        get { return isDying; }
        set
        {
            isDying = value;
        }
    }


}
