using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Represents a player's controllers, can be hooked up the controllable objects.
 */
public class InputManager : MonoBehaviour
{

    public GameObject controllingObject;
    public GameObject optionsHandler;

    private IControllable controllable;
    private InputMode inputMode;

    private Vector2 cursorPosition;

    public enum InputMode
    {
        Keyboard, Controller
    };

    // Start is called before the first frame update
    void Start()
    {
        cursorPosition = Vector2.zero;
        inputMode = InputMode.Keyboard;
        controllable = controllingObject.GetComponent<IControllable>();
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateInputMode();

        if (inputMode == InputMode.Keyboard)
        {
            CheckKeyboardButtons();
        }

        else if (inputMode == InputMode.Controller)
        {
            CheckControllerButtons();
        }
    }

    void CheckKeyboardButtons()
    {
        cursorPosition.x = Input.mousePosition.x;
        cursorPosition.y = Input.mousePosition.y;

        controllable.UpdateMousePosition(cursorPosition.x, cursorPosition.y);

        float moveXTilt = Input.GetAxis("Horizontal");
        float moveYTilt = Input.GetAxis("Vertical");

        controllable.OnDirectional(moveXTilt, moveYTilt);
        controllable.OnMouseMoved();

        if (Input.GetButtonDown("Jump"))
        {
            controllable.OnJumpDown();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            controllable.OnFire1Down();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            controllable.OnFire2Down();
        }
    }

    void CheckControllerButtons()
    {
        float leftXTilt = Input.GetAxis("LeftJoyHorizontal");
        float leftYTilt = Input.GetAxis("LeftJoyVertical");
        controllable.OnDirectional(leftXTilt, leftYTilt);

        float rightXTilt = Input.GetAxis("RightJoyHorizontal");
        float rightYTilt = Input.GetAxis("RightJoyVertical");
        controllable.OnRightAnalogStick(new Vector2(rightXTilt, rightYTilt));

        if (Input.GetButtonDown("Jump"))
        {
            controllable.OnJumpDown();
        }

        if (Input.GetButton("Fire1"))
        {
            controllable.OnFire1Down();
        }

        if (Input.GetButton("Fir21"))
        {
            controllable.OnFire2Down();
        }
    }

    public InputMode GetInputMode()
    {
        return inputMode;
    }


}
