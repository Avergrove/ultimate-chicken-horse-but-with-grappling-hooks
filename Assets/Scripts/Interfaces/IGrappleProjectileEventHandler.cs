using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IGrappleProjectileEventHandler : IEventSystemHandler
{
    void OnProjectileHit(bool isSuccess, Collider2D collider, Vector2 contactPoint);
}
