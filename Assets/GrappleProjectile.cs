using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Defines the projectile as fired by the <see cref="GrappleGun"/>GrappleGun</see>
/// </summary>
public class GrappleProjectile : MonoBehaviour
{

    [Tooltip("Speed at which the projectile will fly at when instantiated")]
    public float projectileSpeed;
    [Tooltip("How long does the projectile take before it decides to retract")]
    public float projectileLifetime;

    private GrappleGun gunSource;
    private Rigidbody2D rgbd;
    private Collider2D coll2D;
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        gunSource = GetComponentInParent<GrappleGun>();
        rgbd = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<Collider2D>();
        this.transform.position = gunSource.transform.position;
        Inactivate();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.Equals(State.Travelling))
        {
            // No management needed
        }

        else if (state.Equals(State.Retracting))
        {
            // Refresh retracting direction
            Retract();
        }
    }

    /// <summary>
    /// Causes  the projectile to start travelling
    /// </summary>
    /// <param name="direction">The direction in which the projectile should travel to</param>
    public void Ignite(Vector2 direction)
    {
        state = State.Travelling;
        this.gameObject.SetActive(true);
        this.transform.position = gunSource.transform.position;
        this.rgbd.velocity = direction * projectileSpeed;
        StartCoroutine(LifetimeRoutine(this.projectileLifetime));
    }

    /// <summary>
    /// Retracts the projectile back towards the grapple gun
    /// </summary>
    void Retract()
    {
        Vector2 dir = (gunSource.transform.parent.position - this.transform.position).normalized;
        this.rgbd.velocity = dir * projectileSpeed;
        state = State.Retracting;
    }

    /// <summary>
    /// Inactivates the grappling gun, stopping it from doing things.
    /// </summary>
    void Inactivate()
    {
        this.gameObject.SetActive(false);
        this.rgbd.velocity = Vector2.zero;
        state = State.Inactive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state.Equals(State.Travelling))
        {
            if (collision.gameObject.CompareTag(Constants.Tags.Grappleable.ToString()))
            {
                ExecuteEvents.Execute<IGrappleProjectileEventHandler>(gunSource.gameObject, null, (handler, data) => handler.OnProjectileHit(true, collision.collider, collision.GetContact(0).point));
                Inactivate();
            }
            
            // Retract when hitting an ungrappleable platform
            else
            {
                Retract();
            }
        }

    }

    // On Trigger enter with collider, check whether collider is valid and latch onto it, if failed, have the hook return to the user.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state.Equals(State.Travelling))
        {
            if (collision.CompareTag(Constants.Tags.Grappleable.ToString()))
            {

                ExecuteEvents.Execute<IGrappleProjectileEventHandler>(gunSource.gameObject, null, (handler, data) => handler.OnProjectileHit(true, collision, collision.ClosestPoint(this.transform.position)));
                Inactivate();
            }

        }

        else if (state.Equals(State.Retracting))
        {
            if (collision.CompareTag(Constants.Tags.Player.ToString()))
            {
                Inactivate();
            }
        }
    }

    private enum State
    {
        Inactive,
        Travelling,
        Retracting
    }

    private IEnumerator LifetimeRoutine(float lifeTime)
    {
        if (this.state.Equals(State.Travelling))
        {
            yield return new WaitForSeconds(lifeTime);
            Retract();
        }
    }
}
