using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerController : MonoBehaviour, IControllable
{

    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDirectional(float x, float y)
    {
        
    }

    public void OnEscDown()
    {
        // Show UI if hidden, hide UI if visible.
        if (!uiManager.HasActivePanels())
        {
            uiManager.ShowUI();
        }

        else
        {
            uiManager.Back();
        }
    }

    public void OnFire1Down()
    {
        throw new System.NotImplementedException();
    }

    public void OnFire1Up()
    {
        throw new System.NotImplementedException();
    }

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

    public void OnMouseMoved(float mouseXMove, float mouseYMove)
    {
        
    }

    public void OnRightAnalogStick(Vector2 tilt)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMousePosition(float x, float y)
    {
        
    }
}
