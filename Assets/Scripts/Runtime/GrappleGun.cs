using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Grants the user the ability to fire grappling hook ropes
/// TODO: Add Elasticity
/// </summary>
public class GrappleGun : MonoBehaviour, IMouseEventManager, IGrappleProjectileEventHandler
{
    [Header("Crosshair: Config")]
    public GameObject crosshair;
    public float crosshairDistance;

    private Player player;
    private Rigidbody2D parentRgbd;

    public GameObject ropeAnchor;
    private SpriteRenderer ropeAnchorSprite;

    // Projectile 
    public GrappleProjectile grappleProjectile;

    // Rope properties
    private SpringJoint2D ropeJoint;

    [Header("Rope: Physical Properties")]
    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;

    public float ropeDamping;
    public float ropeFrequency;


    private bool isColliding;

    [Header("Rope: Movement Stats")]
    public float swingForce;
    public float rappelSpeed;



    void Awake()
    {
        player = GetComponentInParent<Player>();
        ropeJoint = player.gameObject.AddComponent<SpringJoint2D>();
        ConfigureGrappleJoint(ropeJoint);
        ropeAnchor.transform.parent = null;
        parentRgbd = player.GetComponent<Rigidbody2D>();
        ropeAnchorSprite = ropeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player.IsGrappled)
        {
            RefreshRope();
        }
    }

    private void RefreshRope()
    {
        ropeRenderer.SetPosition(0, player.transform.position);
    }

    private void RefreshCrosshairPosition(Vector2 cursorPosition)
    {

        if (!crosshair.activeSelf)
        {
            crosshair.SetActive(true);
        }

        Vector2 aimVector = getAimDirection(cursorPosition);
        float aimRad = Mathf.Atan2(aimVector.y, aimVector.x);
        float x = player.transform.position.x + crosshairDistance * Mathf.Cos(aimRad);
        float y = player.transform.position.y + crosshairDistance * Mathf.Sin(aimRad);

        crosshair.transform.position = new Vector2(x, y);
    }



    /// <summary>
    /// Fires the grappling gun projectile
    /// </summary>
    /// <param name="cursorPosition">The position of the cursor on the screen</param>
    /// <returns>Whether the fire successfully hooked to an object</returns>
    /// TODO: Adjust frequency according to trajectory, 
    public void Fire(Vector2 cursorPosition)
    {
        Vector2 aimVect = getAimDirection(cursorPosition);
        grappleProjectile.Ignite(aimVect);
    }

    public void DetachRope()
    {
        ResetRope();
    }


    // 6
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeRenderer.positionCount = 0;
        ropeRenderer.enabled = false;
        ropeAnchorSprite.enabled = false;
    }

    /// <summary>
    /// Causes the attached object to swing along the rope
    /// </summary>
    /// <param name="x">The directional x axis input</param>
    /// <param name="y">The directional y axis input</param>
    public void Swing(float x)
    {
        if (x < 0 || x > 0)
        {
            var playerToHookDirection = ((Vector2)ropeAnchor.transform.position - (Vector2)player.transform.position).normalized;
            Vector2 swingDirection;
            if (x < 0)
            {
                if (player.transform.position.y <= ropeAnchor.transform.position.y)
                {
                    swingDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                }

                else
                {
                    swingDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                }
            }
            else
            {
                if (player.transform.position.y <= ropeAnchor.transform.position.y)
                {
                    swingDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                }
                else
                {
                    swingDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                }
            }

            var force = swingDirection * swingForce;
            parentRgbd.AddForce(force);
        }
    }

    /// <summary>
    /// Allows the owner to lengthen or shorten the rope
    /// </summary>
    /// <param name="y"></param>
    public void Rappel(float y)
    {
        if (!isColliding && Mathf.Abs(y) > 0)
        {
            ropeJoint.distance -= Time.deltaTime * rappelSpeed * y;
        }
    }

    /// <summary>
    /// Collission detection to check whether rope should continue to be allowed to rappel
    /// </summary>
    /// <param name="colliderStay"></param>
    void OnTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }

    void IMouseEventManager.OnMouseMoved(Vector2 mousePos)
    {
        RefreshCrosshairPosition(mousePos);
    }

    private Vector2 getAimDirection(Vector2 cursorPos)
    {
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(cursorPos.x, cursorPos.y, 0f));
        Vector2 aimDirection = ((Vector2)worldMousePosition - (Vector2)player.transform.position).normalized;

        return aimDirection;
    }

    private void ConfigureGrappleJoint(SpringJoint2D sj)
    {
        sj.connectedBody = ropeAnchor.GetComponent<Rigidbody2D>();
        sj.autoConfigureDistance = false;
        sj.dampingRatio = ropeDamping;
        sj.frequency = ropeFrequency;
        sj.enabled = false;
    }

    void IGrappleProjectileEventHandler.OnProjectileHit(bool isSuccess, Collider2D collider, Vector2 contactPoint)
    {
        if (isSuccess)
        {
            // Latch onto the position of whatever the projectile hit
            GrappleToPoint(contactPoint);
        }

        else
        {
            // I don't think anything happens if you fail, you still need to reload either way

        }

        // Spread the event to the owner too
        ExecuteEvents.Execute<IGrappleProjectileEventHandler>(player.gameObject, null, (handler, data) => handler.OnProjectileHit(isSuccess, collider, contactPoint));
    }

    private void GrappleToPoint(Vector2 grapplePoint)
    {

        ropeRenderer.enabled = true;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, player.transform.position);
        ropeRenderer.SetPosition(1, grapplePoint);


        ropeAnchor.transform.position = grapplePoint;
        ropeJoint.distance = Vector2.Distance(player.transform.position, grapplePoint);
        ropeJoint.enabled = true;
        ropeAnchorSprite.enabled = true;


    }
}
