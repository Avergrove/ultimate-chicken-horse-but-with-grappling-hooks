using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void OnMouseMoved();
    void OnHorizontalAxis(float value);
    void OnVerticalAxis(float value);
    void OnLeftAnalogStick(Vector2 tilt);
    void OnRightAnalogStick(Vector2 tilt);
    void OnKeyboardFireDown();
    void OnKeyboardFireUp();
    void OnJoystickFireDown();
    void OnJoystickFireUp();
    void OnFireDown();
    void OnFireUp();
    void OnJumpDown();
    void OnJumpUp();
}
