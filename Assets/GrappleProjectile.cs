using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the projectile as fired by the <see cref="GrappleGun"/>GrappleGun</see>
/// </summary>
public class GrappleProjectile : MonoBehaviour
{

    GrappleGun gunSource;

    [Tooltip("Speed at which the projectile will fly at when instantiated")]
    public float projectileSpeed;
    
    [Tooltip("Projectile collision mask, enable to have the projectile collide with it.")]
    public LayerMask collisionMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On Trigger enter with collider, check whether collider is valid and latch onto it, if failed, have the hook return to the user.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        // Check tag: Latchable

        
    }
}
