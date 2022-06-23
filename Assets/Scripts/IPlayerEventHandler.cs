using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayerEventHandler : IEventSystemHandler
{
    void OnMovingChange(bool isMoving, Direction dir);
    /// <summary>
    /// Event called when the player becomes grounded or ungrounded
    /// </summary>
    /// <param name="isGrounded">The new state of isGrounded</param>
    void OnGroundedChange(bool isGrounded);
    void OnJump();
    void OnGrappledChange(bool isGrappled);
}
