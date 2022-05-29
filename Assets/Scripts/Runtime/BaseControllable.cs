using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A abstract controllable that is used to handle more universal commands such as operating a user interface.
/// </summary>
public abstract class BaseControllable : MonoBehaviour, IControllable
{
    public abstract void OnDirectional(float x, float y);

    /// <summary>
    /// Delegates the controller to a UI manager controller when escape is pressed, and return it when done.
    /// </summary>
    public void OnEscDown()
    {

    }

    public abstract void OnFire1Down();
    public abstract void OnFire1Up();
    public abstract void OnFire2Down();
    public abstract void OnJoystickFireDown();
    public abstract void OnJoystickFireUp();
    public abstract void OnJumpDown();
    public abstract void OnJumpUp();
    public abstract void OnKeyboardFireDown();
    public abstract void OnKeyboardFireUp();
    public abstract void OnLeftAnalogStick(Vector2 tilt);
    public abstract void OnMouseMoved(float mouseXMove, float mouseYMove);
    public abstract void OnRightAnalogStick(Vector2 tilt);
    public abstract void UpdateMousePosition(float x, float y);
}
