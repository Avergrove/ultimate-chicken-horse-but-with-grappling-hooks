using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// An interface for an inputController to control the player.
/// </summary>
public class PlayerControllable : BaseControllable
{
    public Player player;
    public PlayerAction playerMovement;

    private Vector2 cursorPosition;

    void Start()
    {
        cursorPosition = new Vector2();
    }

    public override void UpdateMousePosition(float x, float y)
    {
        this.cursorPosition.x = x;
        this.cursorPosition.y = y;
        foreach(Transform child in transform)
        {
            ExecuteEvents.Execute<IMouseEventManager>(child.gameObject, null, (handler, data) => handler.OnMouseMoved(this.cursorPosition));
        }
    }

    public void OnMouseMoved()
    {
        // Nothing happens
    }

    public override void OnDirectional(float x, float y)
    {
        playerMovement.Move(x, y);
    }

    public override void OnLeftAnalogStick(Vector2 tilt)
    {
        throw new System.NotImplementedException();
    }

    public override void OnRightAnalogStick(Vector2 tilt)
    {
        throw new System.NotImplementedException();
    }

    public override void OnKeyboardFireDown()
    {
        throw new System.NotImplementedException();
    }

    public override void OnKeyboardFireUp()
    {
        throw new System.NotImplementedException();
    }

    public override void OnJoystickFireDown()
    {
        throw new System.NotImplementedException();
    }

    public override void OnJoystickFireUp()
    {
        throw new System.NotImplementedException();
    }

    public override void OnFire1Down()
    {
        playerMovement.FireGrapplingHook(cursorPosition);
    }

    public override void OnFire2Down()
    {
        playerMovement.ReleaseGrapplingHook(cursorPosition);
    }

    public override void OnFire1Up()
    {
        // Nothing happens.
    }

    public override void OnJumpDown()
    {
        playerMovement.Jump();
    }

    public override void OnJumpUp()
    {
        // Nothing happens
    }

    public override void OnMouseMoved(float mouseXMove, float mouseYMove)
    {
        
    }
}
