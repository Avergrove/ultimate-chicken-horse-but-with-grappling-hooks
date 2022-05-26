using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for an inputController to control the player.
/// </summary>
public class PlayerControllable : MonoBehaviour, IControllable
{
    public Player player;
    public PlayerMovement playerMovement;

    private Vector2 cursorPosition;

    void Start()
    {
        cursorPosition = new Vector2();
    }

    public void UpdateMousePosition(float x, float y)
    {
        this.cursorPosition.x = x;
        this.cursorPosition.y = y;
    }

    public void OnMouseMoved()
    {
        // Nothing happens
    }

    public void OnHorizontalAxis(float value)
    {
        playerMovement.Move(new Vector2(value, 0));
    }

    public void OnVerticalAxis(float value)
    {
        // Nothing happens... yet
    }

    public void OnLeftAnalogStick(Vector2 tilt)
    {
        throw new System.NotImplementedException();
    }

    public void OnRightAnalogStick(Vector2 tilt)
    {
        throw new System.NotImplementedException();
    }

    public void OnKeyboardFireDown()
    {
        throw new System.NotImplementedException();
    }

    public void OnKeyboardFireUp()
    {
        throw new System.NotImplementedException();
    }

    public void OnJoystickFireDown()
    {
        throw new System.NotImplementedException();
    }

    public void OnJoystickFireUp()
    {
        throw new System.NotImplementedException();
    }

    public void OnFire1Down()
    {
        playerMovement.FireGrapplingHook(cursorPosition);
    }

    public void OnFire2Down()
    {
        playerMovement.ReleaseGrapplingHook(cursorPosition);
    }

    public void OnFire1Up()
    {
        // Nothing happens.
    }

    public void OnJumpDown()
    {
        playerMovement.Jump();
    }

    public void OnJumpUp()
    {
        // Nothing happens
    }
}
