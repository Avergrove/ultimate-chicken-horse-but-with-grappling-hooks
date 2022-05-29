using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Represents a player's controllers, can be hooked up to controllable objects.
 */
public class InputManager : MonoBehaviour
{
    private Mode mode;
    public UIManager uiManager;

    [Tooltip("A gameobject that contains a component that implements IControllable")]
    public GameObject initControlling;
    private IControllable controlling;
    private UIManagerController uiManagerController;

    private InputMode inputMode;

    private Vector2 cursorPosition;

    public enum InputMode
    {
        Keyboard, Controller
    };

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.Controllable;
        cursorPosition = Vector2.zero;
        inputMode = InputMode.Keyboard;
        controlling = initControlling.GetComponent<IControllable>();
        uiManagerController = uiManager.GetComponent<UIManagerController>();
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
        // Object navigation mode
        if (mode == Mode.Controllable)
        {
            cursorPosition.x = Input.mousePosition.x;
            cursorPosition.y = Input.mousePosition.y;

            controlling.UpdateMousePosition(cursorPosition.x, cursorPosition.y);

            float moveXTilt = Input.GetAxis("Horizontal");
            float moveYTilt = Input.GetAxis("Vertical");

            controlling.OnDirectional(moveXTilt, moveYTilt);

            float mouseXMove = Input.GetAxis("Mouse X");
            float mouseYMove = Input.GetAxis("Mouse Y");
            if (mouseXMove != 0 || mouseYMove != 0)
            {
                controlling.OnMouseMoved(mouseXMove, mouseYMove);
            }

            if (Input.GetButtonDown("Jump"))
            {
                controlling.OnJumpDown();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                controlling.OnFire1Down();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                controlling.OnFire2Down();
            }

            if (Input.GetButtonDown("Pause"))
            {
                if (mode == Mode.Controllable)
                {
                    mode = Mode.Menu;
                    Time.timeScale = 0;
                    uiManagerController.OnEscDown();
                }

                else if (mode == Mode.Menu)
                {
                    mode = Mode.Controllable;
                }
            }

        }

        // Menu navigation mode
        else if (mode == Mode.Menu)
        {
            if (Input.GetButtonDown("Pause"))
            {
                uiManagerController.OnEscDown();
                if (!uiManager.HasActivePanels())
                {
                    mode = Mode.Controllable;
                    Time.timeScale = 1;
                }
            }
        }
    }

    void CheckControllerButtons()
    {
        float leftXTilt = Input.GetAxis("LeftJoyHorizontal");
        float leftYTilt = Input.GetAxis("LeftJoyVertical");
        controlling.OnDirectional(leftXTilt, leftYTilt);

        float rightXTilt = Input.GetAxis("RightJoyHorizontal");
        float rightYTilt = Input.GetAxis("RightJoyVertical");
        controlling.OnRightAnalogStick(new Vector2(rightXTilt, rightYTilt));

        if (Input.GetButtonDown("Jump"))
        {
            controlling.OnJumpDown();
        }

        if (Input.GetButton("Fire1"))
        {
            controlling.OnFire1Down();
        }

        if (Input.GetButton("Fir21"))
        {
            controlling.OnFire2Down();
        }
    }

    public InputMode GetInputMode()
    {
        return inputMode;
    }

    public IControllable GetControllable()
    {
        return this.controlling;
    }

    public void SetControllable(IControllable controllable)
    {
        this.controlling = controllable;
    }

    /// <summary>
    /// Mode enum that represents what the input manager is currently controlling.
    /// </summary>
    private enum Mode
    {
        Controllable, Menu
    }

}
