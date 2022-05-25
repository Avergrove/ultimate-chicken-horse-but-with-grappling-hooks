using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllable : MonoBehaviour, IControllable
{
    public Player player;

    public void OnMouseMoved()
    {
        // Nothing happens
    }

    public void OnHorizontalAxis(float value)
    {
        player.Move(new Vector2(value, 0));
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

    public void OnFireDown()
    {
        // Nothing happens... yet
    }

    public void OnFireUp()
    {
        // Nothing happens.
    }

    public void OnJumpDown()
    {
        player.Jump();
    }

    public void OnJumpUp()
    {
        // Nothing happens
    }
}
