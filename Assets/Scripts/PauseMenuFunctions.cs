using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuFunctions : MenuFunctions
{

    public InputManager inputManager;
    public PlatformEditorController platformEditorControllable;
    public Text textPanel;
    public GameObject gridOverlay;


    private string panelPlaceholder;
    
    private IControllable prevControllable;

    private void Start()
    {
        panelPlaceholder = "Currently Controlling: {0}";
        textPanel.text = string.Format(panelPlaceholder, "Default");
    }

    // Pause Panel functions
    /// <summary>
    /// Swap controller mode between input mode and controllable mode.
    /// </summary>
    public void OnSwapInputManagerButtonClick()
    {
        if (prevControllable == null) {
            EnterEditMode();
        }

        else
        {
            ExitEditMode();
        }
    }

    /// <summary>
    /// Returns the player to the main menu scene. Well, there isn't one yet anyway.
    /// </summary>
    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("mainMenu");
        if (!op.isDone)
        {
            yield return null;
        }
    }

    private void EnterEditMode()
    {
        prevControllable = inputManager.GetControllable();
        inputManager.SetControllable(platformEditorControllable);
        platformEditorControllable.gameObject.SetActive(true);
        textPanel.text = string.Format(panelPlaceholder, platformEditorControllable.name);
        gridOverlay.SetActive(true);
    }

    
    private void ExitEditMode()
    {
        inputManager.SetControllable(prevControllable);
        platformEditorControllable.gameObject.SetActive(false);
        prevControllable = null;
        textPanel.text = string.Format(panelPlaceholder, "Default");
        gridOverlay.SetActive(false);
    }
}
