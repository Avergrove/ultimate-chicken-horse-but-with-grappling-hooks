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


    public enum InputMode
    {
        Keyboard, Controller
    };

    // Start is called before the first frame update
    void Start()
    {
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

        float moveXTilt = Input.GetAxis("Horizontal");
        float moveYTilt = Input.GetAxis("Vertical");

        controllable.OnHorizontalAxis(moveXTilt);
        controllable.OnVerticalAxis(moveYTilt);
        controllable.OnMouseMoved();

        if (Input.GetButtonDown("Jump"))
        {
            controllable.OnJumpDown();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            controllable.OnFireDown();
        }
    }

    void CheckControllerButtons()
    {
        float leftXTilt = Input.GetAxis("LeftJoyHorizontal");
        float leftYTilt = Input.GetAxis("LeftJoyVertical");
        controllable.OnLeftAnalogStick(new Vector2(leftXTilt, leftYTilt));

        float rightXTilt = Input.GetAxis("RightJoyHorizontal");
        float rightYTilt = Input.GetAxis("RightJoyVertical");
        controllable.OnRightAnalogStick(new Vector2(rightXTilt, rightYTilt));

        if (Input.GetButtonDown("Jump"))
        {
            controllable.OnJumpDown();
        }

        if (Input.GetButton("Fire"))
        {
            controllable.OnFireDown();
        }
    }

    public InputMode GetInputMode()
    {
        return inputMode;
    }


}
