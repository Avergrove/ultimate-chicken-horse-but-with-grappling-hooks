using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEditorController : MonoBehaviour, IControllable
{
    PlatformEditor platformEditor;
    private Vector2 mousePosition;

    void Start()
    {
        platformEditor = GetComponent<PlatformEditor>();
        mousePosition = new Vector2();
    }

    public void OnDirectional(float x, float y)
    {
        // TODO: Implement controller movement
    }

    /// <summary>
    /// Deploys a block
    /// </summary>
    public void OnFire1Down()
    {
        platformEditor.DeployPlatform(mousePosition);
    }

    public void OnFire1Up()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Destroys a selected block that was deployed
    /// </summary>
    public void OnFire2Down()
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

    public void OnJumpDown()
    {
        throw new System.NotImplementedException();
    }

    public void OnJumpUp()
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

    public void OnLeftAnalogStick(Vector2 tilt)
    {
        throw new System.NotImplementedException();
    }

    public void OnRightAnalogStick(Vector2 tilt)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMousePosition(float x, float y)
    {
        this.mousePosition.x = x;
        this.mousePosition.y = y;
    }

    public void OnMouseMoved(float mouseXMove, float mouseYMove)
    {
        platformEditor.MoveCursor(mousePosition);
    }

    public void OnEscDown()
    {
        throw new System.NotImplementedException();
    }
}
